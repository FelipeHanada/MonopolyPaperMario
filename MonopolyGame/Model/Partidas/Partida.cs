using MonopolyGame.Impl.Cartas;
using MonopolyGame.Impl.Efeitos;
using MonopolyGame.Interface.Cartas;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Interface.Partidas;
using MonopolyGame.Interface.PosseJogador;
using MonopolyGame.Model.Cartas;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PossesJogador;
using MonopolyGame.Model.PropostasTroca;
using MonopolyGame.Model.Tabuleiros;
using MonopolyGame.Model.Historicos;
using MonopolyGame.Utils;
using System.Drawing;
using MonopolyGame.Model.Leiloes;

namespace MonopolyGame.Model.Partidas;

public class Partida
{
    public List<Jogador> Jogadores { get; }
    public List<Jogador> JogadoresAtivos { get => [.. Jogadores.Where(j => j.Falido == false)]; }
    public Historico Historico { get; }
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
        Historico = new Historico();

        EstadoTurnoAtual = new EstadoTurnoComum(JogadorAtual);
        AdicionarRegistro($"A partida começou!");
        AdicionarRegistro($"Novo Turno: {JogadorAtual.Nome}");

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
        Piso[] pisos = [
            new Piso("Ponto de Partida"),
            new PisoCompravel("Goomba Village", new Imovel("Goomba Village", 60, 30, PropriedadeCor.Marrom, [2, 10, 30, 90, 160, 250], 50, 25)),
            new PisoComprarCartaCofre("Baú Comunitário", DeckCofre),
            new PisoCompravel("Koopa Village", new Imovel("Koopa Village", 60, 30, PropriedadeCor.Marrom, [4, 20, 60, 180, 320, 450], 50, 25)),
            new PisoTaxaRiquesa("Taxa de Riquesa", 200),
            new PisoCompravel("Toad Town Station", new LinhaTrem("Toad Town Station")),
            new PisoCompravel("Mt Rugged", new Imovel("Mt Rugged", 100, 50, PropriedadeCor.Ciano, [6, 30, 90, 270, 400, 550], 50, 25)),
            new PisoComprarCartaSorte("Sorte ou Revés", DeckSorte),
            new PisoCompravel("Dry Dry Outpost", new Imovel("Dry Dry Outpost", 100, 50, PropriedadeCor.Ciano, [6, 30, 90, 270, 400, 550], 50, 25)),
            new PisoCompravel("Dry Dry Ruins", new Imovel("Dry Dry Ruins", 120, 60, PropriedadeCor.Ciano, [8, 40, 100, 300, 450, 600], 50, 25)),

            new PisoCadeia("Cadeia"),
            new PisoCompravel("Boo's Mansion", new Imovel("Boo's Mansion", 140, 70, PropriedadeCor.Rosa, [10, 50, 150, 450, 625, 750], 100, 50)),
            new PisoCompravel("Big Lantern Ghost", new Companhia("Big Lantern Ghost")),
            new PisoCompravel("Gusty Gulch", new Imovel("Gusty Gulch", 140, 70, PropriedadeCor.Rosa, [10, 50, 150, 450, 625, 750], 100, 50)),
            new PisoCompravel("Tubba Blubba's Castle", new Imovel("Tubba Blubba's Castle", 160, 80, PropriedadeCor.Rosa, [12, 60, 180, 500, 700, 900], 100, 50)),
            new PisoCompravel("Mt.Rugged Station", new LinhaTrem("Mt.Rugged Station")),
            new PisoCompravel("Toad Town Tunnels", new Imovel("Toad Town Tunnels", 180, 90, PropriedadeCor.Laranja, [14, 70, 200, 550, 750, 950], 100, 50)),
            new PisoComprarCartaCofre("Baú Comunitário", DeckCofre),
            new PisoCompravel("Shooting Star Summit", new Imovel("Shooting Star Summit", 180, 90, PropriedadeCor.Laranja, [14, 70, 200, 550, 750, 950], 100, 50)),
            new PisoCompravel("Shy Guy's Toybox", new Imovel("Shy Guy's Toybox", 200, 100, PropriedadeCor.Laranja, [16, 80, 220, 600,800, 1000], 100, 50)),

            new Piso("Parada Livre"),
            new PisoCompravel("Yoshi's Village", new Imovel("Yoshi's Village", 220, 110, PropriedadeCor.Vermelho, [18, 90, 250, 700, 875, 1050], 150, 75)),
            new PisoComprarCartaSorte("Sorte ou Revés", DeckSorte),
            new PisoCompravel("Jade Jungle", new Imovel("Jade Jungle", 220, 110, PropriedadeCor.Vermelho, [18, 90, 250, 700, 875, 1050], 150, 75)),
            new PisoCompravel("Mt Lavalava", new Imovel("Mt Lavalava", 240, 120, PropriedadeCor.Vermelho, [20, 100, 300, 750, 925, 1100], 150, 75)),
            new PisoCompravel("Toy Box Station", new LinhaTrem("Toy Box Station")),
            new PisoCompravel("Flower Fields", new Imovel("Flower Fields", 260, 130, PropriedadeCor.Amarelo, [22, 110, 330, 800, 975, 1150], 150, 75)),
            new PisoCompravel("Sun Tower", new Imovel("Sun Tower", 260, 130, PropriedadeCor.Amarelo, [22, 110, 330, 800, 975, 1150], 150, 75)),
            new PisoCompravel("Paper Mario Whale", new Companhia("Paper Mario Whale")),
            new PisoCompravel("Cloudy Climb", new Imovel("Cloudy Climb", 280, 140, PropriedadeCor.Amarelo, [24, 120, 360, 850, 1025, 1200], 150, 75)),

            new Piso("Vá para a cadeia", new EfeitoIrParaCadeia()),
            new PisoCompravel("Shiver City", new Imovel("Shiver City", 300, 150, PropriedadeCor.Verde, [26, 130, 390, 900, 1100, 1275], 200, 100)),
            new PisoCompravel("Starborn Valley", new Imovel("Starborn Valley", 300, 150, PropriedadeCor.Verde, [26, 130, 390, 900, 1100, 1275], 200, 100)),
            new PisoComprarCartaCofre("Baú Comunitário", DeckCofre),
            new PisoCompravel("Crystal Palace", new Imovel("Crystal Palace", 320, 160, PropriedadeCor.Verde, [28, 150, 450, 1000, 1200, 1400], 200, 100)),
            new PisoCompravel("Star Crusier Station", new LinhaTrem("Star Crusier Station")),
            new PisoComprarCartaSorte("Sorte ou Revés", DeckSorte),
            new PisoCompravel("Star Heaven", new Imovel("Star Heaven", 350, 175, PropriedadeCor.Azul, [35, 175, 500, 1100, 1300, 1500], 200, 100)),
            new PisoTaxaRiquesa("Taxa de Riquesa", 100),
            new PisoCompravel("Bowser's Castle", new Imovel("Bowser's Castle", 400, 200, PropriedadeCor.Azul, [50, 200, 600, 1400, 1700, 2000], 200, 100)),
        ];

        return pisos;
    }

    public void AddEfeitoTurnoParaJogadores(int turnos, IEfeitoJogador efeito, Jogador[] Jogadores)
    {
        Log.WriteLine("==================DEBUG===============\nAdicionado um efeito agendado.");
        EfeitosAReverter.Add([turnos, efeito, Jogadores]);
    }

    // ================================================================================================================

    //public bool RolarDados(out (int, int) dados, out int posicaoFinal)
    //{
    //    return EstadoTurnoAtual.RolarDados(out dados, out posicaoFinal);
    //}
    //public bool HipotecarPropriedade(Propriedade propriedade)
    //{
    //    return EstadoTurnoAtual.HipotecarPropriedade(propriedade);
    //}
    //public bool MelhorarImovel(Imovel imovel)
    //{
    //    return EstadoTurnoAtual.MelhorarImovel(imovel);
    //}
    //public bool DepreciarImovel(Imovel imovel)
    //{
    //    return EstadoTurnoAtual.DepreciarImovel(imovel);
    //}
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

        if (JogadorAtual.Dinheiro < 0)
        {
            JogadorAtual.Falido = true;
        }

        do
        {
            JogadorAtualIndex = (JogadorAtualIndex + 1) % Jogadores.Count;
        } while (Jogadores[JogadorAtualIndex].Falido);

        EstadoTurnoAtual = new EstadoTurnoComum(JogadorAtual);

        AdicionarRegistro($"Novo Turno: {JogadorAtual.Nome}");
        return true;
    }

    public bool IniciarPropostaTroca(PropostaTroca proposta, bool comecarLeilaoSeRecusado = false)
    {
        if (!EstadoTurnoAtual.PodeIniciarPropostaTroca) return false;
        EstadoTurnoAtual = (comecarLeilaoSeRecusado ? new EstadoTurnoPropostaTrocaComLeilao(JogadorAtual, proposta, proposta.PossesOfertadas[0]) : new EstadoTurnoPropostaTroca(JogadorAtual, proposta));
        return true;
    }

    public bool EncerrarPropostaTroca(bool aceite)
    {
        if (EstadoTurnoAtual.EstadoId != EstadoTurnoId.PropostaTroca)
            return false;

        EstadoTurnoAtual.EncerrarPropostaTroca(aceite);
        return true;
    }

    public bool IniciarLeilao(Leilao leilao)
    {
        if (EstadoTurnoAtual.EstadoId != EstadoTurnoId.Comum) return false;
        EstadoTurnoAtual = new EstadoTurnoLeilao(JogadorAtual, leilao);
        return true;
    }

    public bool EncerrarLeilao()
    {
        if (EstadoTurnoAtual.EstadoId != EstadoTurnoId.Leilao || !EstadoTurnoAtual.Leilao.Finalizado || EstadoTurnoAtual.Leilao.MaiorLicitante == null)
            return false;

        Jogador vencedor = EstadoTurnoAtual.Leilao.MaiorLicitante;

        EstadoTurnoAtual.Leilao.PosseJogador.Proprietario?.RemoverPosse(EstadoTurnoAtual.Leilao.PosseJogador);
        vencedor.AdicionarPosse(EstadoTurnoAtual.Leilao.PosseJogador);

        EstadoTurnoAtual = new EstadoTurnoComum(JogadorAtual);
        return true;
    }

    public void AdicionarRegistro(string jogada)
    {
        Historico.AddRegistro(jogada);
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
