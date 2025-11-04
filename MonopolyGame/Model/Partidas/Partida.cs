using MonopolyGame.Model.Cartas;
using MonopolyGame.Model.Tabuleiros;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Interface.Cartas;
using MonopolyGame.Impl.Cartas;
using MonopolyGame.Impl.Efeitos;

namespace MonopolyGame.Model.Partidas;

public class Partida
{
    private List<object[]> efeitosAReverter;
    public List<Jogador> jogadores { get; private set; }
    public Tabuleiro tabuleiro { get; private set; }
    public int casasDisponiveis { get; private set; } = 32;
    public int hoteisDisponiveis { get; private set; } = 12;
    public Jogador? jogadorAtual => jogadorAtualIndex >= 0 && jogadorAtualIndex < jogadores.Count ? jogadores[jogadorAtualIndex] : null;
    private int jogadorAtualIndex;

    public IDeck? deckCofre, deckSorte;

    public void addEfeitoTurnoParaJogadores(int turnos, IEfeitoJogador efeito, Jogador[] jogadores)
    {
        Console.WriteLine("==================DEBUG===============\nAdicionado um efeito agendado.");
        efeitosAReverter.Add([turnos, efeito, jogadores]);
    }

    public Partida(List<string> nomes)
    {
        jogadores = [];
        foreach (string nome in nomes)
        {
            jogadores.Add(new Jogador(nome));
        }
        jogadorAtualIndex = -1;
        efeitosAReverter = [];

        (deckCofre, deckSorte) = CriarDecks();
        tabuleiro = CriarTabuleiro();
        foreach (Jogador jogador in jogadores)
        {
            tabuleiro.AddJogador(jogador);
        }

        Console.WriteLine("A partida começou!");
    }

    public Tabuleiro GetTabuleiro()
    {
        return tabuleiro;
    }

    private void AdicionarJogador(string nome)
    {
        if (jogadores.Count < 6)
        {
            Jogador novoJogador = new Jogador(nome);
            jogadores.Add(novoJogador);
            Console.WriteLine($"Jogador {nome} adicionado.");
        }
    }

    public List<Jogador> GetJogadores()
    {
        return jogadores;
    }

    public List<Jogador> GetJogadoresAtivos()
    {
        return [.. jogadores.Where(j => j.Falido == false)];
    }

    public void ProximoTurno()
    {
        if (jogadorAtualIndex == 0) // para só reverter depois que todo mundo jogar. Uma rodada.
        {
            int i = 0;
            while (i < efeitosAReverter.Count) // para cada efeito agendado
            {
                object[] efeitoAtual = efeitosAReverter[i];
                efeitoAtual[0] = (int)efeitoAtual[0] - 1;

                if ((int)efeitoAtual[0] == -1)
                {
                    foreach (Jogador jogador in (Jogador[])efeitoAtual[2]) // para cada jogador afetado
                    {
                        ((IEfeitoJogador)efeitoAtual[1]).Aplicar(jogador);
                    }
                    efeitosAReverter.RemoveAt(i);
                    Console.WriteLine("==================DEBUG===============\nEfeito revertido.");
                }
                else
                {
                    Console.WriteLine("==================DEBUG===============\nAinda não é para reverter.");
                    i++;
                }
            }

        }

        if (jogadores.Count(j => !j.Falido) <= 1) return;

        do
        {
            jogadorAtualIndex = (jogadorAtualIndex + 1) % jogadores.Count;
        } while (jogadores[jogadorAtualIndex].Falido);
    }

    private (IDeck, IDeck) CriarDecks()
    {
        FabricaAbstrataCartaCofre fabricaCartaCofre = new FabricaCartaCofre(this);
        FabricaAbstrataCartaSorte fabricaCartaSorte = new FabricaCartaSorte(this);

        var cartasCofre = fabricaCartaCofre.CriaTodasAsCartas();
        var cartasSorte = fabricaCartaSorte.CriaTodasAsCartas();
        
        var deckCofre = new Deck<CartaCofre>(cartasCofre);
        var deckSorte = new Deck<CartaSorte>(cartasSorte);

        return (deckCofre, deckSorte);
    }

    private Tabuleiro CriarTabuleiro()
    {
        Tabuleiro tabuleiro = new(CriarPisos(), jogadores);
        tabuleiro.SetPiso(30, new Piso("Vá para a Cadeia", new EfeitoIrParaCadeia(this)));
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

            if (i % 2 == 0 && deckSorte != null)
            {
                pisos[i] = new Piso("Sorte ou Revés", new EfeitoComprarCarta(this, deckSorte));
            }
            else if (deckCofre != null)
            {
                pisos[i] = new Piso("Cofre Comunitário", new EfeitoComprarCarta(this, deckCofre));
            }
        }

        return pisos;
    }
}
