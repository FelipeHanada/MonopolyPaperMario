using MonopolyGame.Utils;
using MonopolyGame.Impl.Cartas;
using MonopolyGame.Impl.Efeitos;
using MonopolyGame.Interface.Cartas;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Interface.Partidas;
using MonopolyGame.Model.Cartas;
using MonopolyGame.Model.PossesJogador;
using MonopolyGame.Model.Tabuleiros;
using MonopolyGame.Model.PropostasTroca;

namespace MonopolyGame.Model.Partidas;

public class Partida
{
    public List<Jogador> Jogadores { get; }
    public List<Jogador> JogadoresAtivos { get => [.. Jogadores.Where(j => j.Falido == false)]; }
    public Tabuleiro Tabuleiro { get; private set; }
    public int CasasDisponiveis { get; private set; } = 32;
    public int HoteisDisponiveis { get; private set; } = 12;
    public Jogador JogadorAtual { get => Jogadores[JogadorAtualIndex]; }
    public int JogadorAtualIndex { get; private set; }
    public IDeck? DeckCofre { get; }
    public IDeck? DeckSorte { get; }

    public IEstadoTurno EstadoTurnoAtual { get; internal set; }

    private readonly List<object[]> EfeitosAReverter;

    public Partida(List<string> nomes)
    {
        Jogadores = [];
        foreach (string nome in nomes)
        {
            Jogador novoJogador = new Jogador(this, nome);
            Jogadores.Add(novoJogador);
        }
        JogadorAtualIndex = 0;
        EfeitosAReverter = [];

        (DeckCofre, DeckSorte) = CriarDecks();
        Tabuleiro = CriarTabuleiro();

        EstadoTurnoAtual = new EstadoTurnoComum(JogadorAtual);

        Log.WriteLine("A partida começou!");
    }

    private (IDeck, IDeck) CriarDecks()
    {
        FabricaAbstrataPropriedade fabricaCartaCofre = new FabricaCartaCofre(this);
        FabricaAbstrataCartaSorte fabricaCartaSorte = new FabricaCartaSorte(this);

        var cartasCofre = fabricaCartaCofre.CriaTodasAsCartas();
        var cartasSorte = fabricaCartaSorte.CriaTodasAsCartas();
        
        var deckCofre = new Deck<CartaCofre>(cartasCofre);
        var deckSorte = new Deck<CartaSorte>(cartasSorte);

        return (deckCofre, deckSorte);
    }

    private Tabuleiro CriarTabuleiro()
    {
        Tabuleiro tabuleiro = new(CriarPisos(), Jogadores);
        tabuleiro.SetPiso(30, new Piso("Vá para a Cadeia", new EfeitoIrParaCadeia()));
        return tabuleiro;
    }

    // Tabuleiro focado em cartas. Deixei o "original" comentado lá embaixo
    private Piso[] CriarPisos()
    {
        var pisos = new Piso[40];
        pisos[0] = new Piso("Ponto de Partida");
        pisos[10] = new Piso("Cadeia (Apenas Visitando)");
        pisos[20] = new Piso("Parada Livre");

        for (int i = 1; i < 40; i++)
        {
            if (pisos[i] != null) continue;

            pisos[i] = new Piso(
                "Piso Comprável: Casa do Mario",
                new EfeitoPropriedadeCompravel(new Imovel("Casa do Mario", 10, "teste", [0, 0, 0, 0, 0, 0], 50, 25))
            );
            //if (i % 2 == 0 && DeckSorte != null)
            //{
            //    pisos[i] = new Piso("Sorte ou Revés", new EfeitoComprarCarta(DeckSorte));
            //}
            //else if (DeckCofre != null)
            //{
            //    pisos[i] = new Piso("Cofre Comunitário", new EfeitoComprarCarta(DeckCofre));
            //}
        }

        return pisos;
    }

    public void AddEfeitoTurnoParaJogadores(int turnos, IEfeitoJogador efeito, Jogador[] Jogadores)
    {
        Log.WriteLine("==================DEBUG===============\nAdicionado um efeito agendado.");
        EfeitosAReverter.Add([turnos, efeito, Jogadores]);
    }

    // ================================================================================================================

    public bool RolarDados(out (int, int) dados, out int posicaoFinal)
    {
        return EstadoTurnoAtual.RolarDados(out dados, out posicaoFinal);
    }
    public bool HipotecarPropriedade(Propriedade propriedade)
    {
        return EstadoTurnoAtual.HipotecarPropriedade(propriedade);
    }
    public bool MelhorarImovel(Imovel imovel)
    {
        return EstadoTurnoAtual.MelhorarImovel(imovel);
    }
    public bool DepreciarImovel(Imovel imovel)
    {
        return EstadoTurnoAtual.DepreciarImovel(imovel);
    }
    public bool FinalizarTurno()
    {
        if (!EstadoTurnoAtual.PodeEncerrarTurno) return false;

        if (JogadorAtualIndex == 0) // para só reverter depois que todo mundo jogar. Uma rodada.
        {
            int i = 0;
            while (i < EfeitosAReverter.Count) // para cada efeito agendado
            {
                object[] efeitoAtual = EfeitosAReverter[i];
                efeitoAtual[0] = (int)efeitoAtual[0] - 1;

                if ((int)efeitoAtual[0] == -1)
                {
                    foreach (Jogador jogador in (Jogador[])efeitoAtual[2]) // para cada jogador afetado
                    {
                        ((IEfeitoJogador)efeitoAtual[1]).Aplicar(jogador);
                    }
                    EfeitosAReverter.RemoveAt(i);
                    Log.WriteLine("==================DEBUG===============\nEfeito revertido.");
                }
                else
                {
                    Log.WriteLine("==================DEBUG===============\nAinda não é para reverter.");
                    i++;
                }
            }

        }

        if (Jogadores.Count(j => !j.Falido) <= 1) return false;

        do
        {
            JogadorAtualIndex = (JogadorAtualIndex + 1) % Jogadores.Count;
        } while (Jogadores[JogadorAtualIndex].Falido);

        EstadoTurnoAtual = new EstadoTurnoComum(JogadorAtual);
        return true;
    }

    public bool IniciarPropostaTroca(PropostaTroca proposta)
    {
        if (!EstadoTurnoAtual.PodeIniciarPropostaTroca) return false;
        EstadoTurnoAtual = new EstadoTurnoPropostaTroca(JogadorAtual, proposta);
        return true;
    }

    public bool EncerrarPropostaTroca(bool aceite)
    {
        if (EstadoTurnoAtual.EstadoId != EstadoTurnoId.PropostaTroca)
            return false;

        EstadoTurnoAtual.EncerrarPropostaTroca(aceite);
        return true;
    }

    public bool IniciarLeilao()
    {
        if (EstadoTurnoAtual.EstadoId != EstadoTurnoId.Comum) return false;
        EstadoTurnoAtual = new EstadoTurnoLeilao(JogadorAtual, posseJogador);
        return true;
    }
    
    //public virtual Leilao GetLeilao()
    //{
    //    return EstadoTurnoAtual.GetLeilao();
    //}
    //public Jogador GetJogadorAtualLeilao()
    //{
    //    return EstadoTurnoAtual.GetJogadorAtualLeilao();
    //}
    //public Jogador DarLanceLeilao(int delta) { throw new NotImplementedException("Ação DarLanceLeilao não implementada ou inválida neste estado."); }
    //public Jogador DesistirLeilao() { throw new NotImplementedException("Ação DesistirLeilao não implementada ou inválida neste estado."); }

    //public virtual PropostaTroca GetPropostaTroca() { throw new NotImplementedException("Ação GetPropostaTroca não implementada ou inválida neste estado."); }
    //public virtual PropostaTroca IniciarPropostaTroca() { throw new NotImplementedException("Ação IniciarPropostaTroca não implementada ou inválida neste estado."); }
    //public virtual void EncerrarPropostaTroca(bool aceite) { throw new NotImplementedException("Ação EncerrarPropostaTroca não implementada ou inválida neste estado."); }
}
