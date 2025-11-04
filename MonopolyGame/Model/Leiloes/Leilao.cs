using MonopolyGame.Interface;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PossesJogador;

namespace MonopolyGame.Model.Leiloes;


public class Leilao
{
    private readonly Partida partida;
    public IPosseJogador PosseJogador{ get; private set; }
    public List<Jogador> Participantes { get; private set; }
    public Jogador JogadorIniciador { get; private set; }
    public int LanceAtual { get; private set; }
    public Jogador? MaiorLicitante { get; private set; }

    public Leilao(Partida partida, IPosseJogador posseJogador, List<Jogador> participantes, Jogador jogadorIniciador)
    {
        this.partida = partida;
        PosseJogador = posseJogador;
        Participantes = participantes;
        JogadorIniciador = jogadorIniciador;
        LanceAtual = 0;
        MaiorLicitante = null;
    }

    public void Executar()
    {

        Jogador? proprietarioOriginal = PosseJogador.Proprietario;
        List<Jogador> licitantesAtivos = new List<Jogador>(Participantes);

        int index = (licitantesAtivos.IndexOf(JogadorIniciador) + 1) % licitantesAtivos.Count;

        while (licitantesAtivos.Count > 1)
        {
            Jogador jogadorAtual = licitantesAtivos[index];

            Console.WriteLine($"\nLance atual: ${LanceAtual} (por {MaiorLicitante?.Nome ?? "ninguém"})");

            if (jogadorAtual.Dinheiro <= LanceAtual)
            {
                Console.WriteLine($"{jogadorAtual.Nome} não tem fundos para continuar e foi removido do leilão.");
                licitantesAtivos.RemoveAt(index);
                if (index >= licitantesAtivos.Count) index = 0;
                continue;
            }

            bool turnoDoJogadorConcluido = false;
            while (!turnoDoJogadorConcluido)
            {
                Console.Write($"{jogadorAtual.Nome} (Saldo: ${jogadorAtual.Dinheiro}), digite seu lance ou 'sair': ");
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "sair")
                {
                    Console.WriteLine($"{jogadorAtual.Nome} saiu do leilão.");
                    licitantesAtivos.RemoveAt(index);
                    if (index >= licitantesAtivos.Count) index = 0;
                    turnoDoJogadorConcluido = true;
                }
                else if (int.TryParse(input, out int novoLance))
                {
                    if (novoLance > LanceAtual && novoLance <= jogadorAtual.Dinheiro)
                    {
                        LanceAtual = novoLance;
                        MaiorLicitante = jogadorAtual;
                        Console.WriteLine($"Novo lance de ${LanceAtual} por {jogadorAtual.Nome}!");
                        turnoDoJogadorConcluido = true;
                    }
                    else if (novoLance <= LanceAtual)
                    {
                        Console.WriteLine("Seu lance deve ser maior que o lance atual. Tente novamente.");
                    }
                    else
                    {
                        Console.WriteLine("Você não tem dinheiro suficiente para este lance. Tente novamente.");
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Tente novamente.");
                }
            }

            if (licitantesAtivos.Contains(jogadorAtual))
            {
                index = (index + 1) % licitantesAtivos.Count;
            }
        }

        if (licitantesAtivos.Count == 1 && MaiorLicitante != null)
        {
            Jogador vencedor = MaiorLicitante;
            Console.WriteLine($"{vencedor.Nome} venceu o leilão de {PosseJogador.Nome} por ${LanceAtual}!");

            vencedor.Debitar(LanceAtual);
            if (proprietarioOriginal != null)
            {
                Console.WriteLine($"O valor de ${LanceAtual} foi pago a {proprietarioOriginal.Nome}.");
                proprietarioOriginal.Creditar(LanceAtual);
                proprietarioOriginal.Posses.Remove(PosseJogador);
            }
            else
            {
                Console.WriteLine("O valor foi pago ao banco.");
            }

            PosseJogador.Proprietario = vencedor;
            vencedor.AdicionarPosse(PosseJogador);
        }
        else
        {
            Console.WriteLine("O leilão terminou sem um vencedor. A PosseJogador permanece com seu dono original ou com o banco.");
        }
    }
}
