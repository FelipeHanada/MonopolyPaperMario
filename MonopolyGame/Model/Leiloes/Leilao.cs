using MonopolyGame.Utils;
using MonopolyGame.Interface;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Model.Leiloes;


public class Leilao
{
    public IPosseJogador PosseJogador { get; private set; }
    public Partida Partida { get; private set; }

    public List<Jogador> Participantes { get; }

    public int MaiorLance { get; private set; }
    public Jogador? MaiorLicitante { get; private set; }

    private int indiceJogadorAtual;
    public Jogador? JogadorAtual { get; private set; }

    public bool Finalizado { get => JogadorAtual == null; }

    public Leilao(Partida partida, IPosseJogador posseJogador)
    {
        Partida = partida;
        PosseJogador = posseJogador;

        Participantes = [.. Partida.JogadoresAtivos];

        MaiorLance = 0;
        MaiorLicitante = null;

        indiceJogadorAtual = 0;
        if (Participantes.Count > 0)
        {
            JogadorAtual = Participantes[indiceJogadorAtual];
        }
        else
        {
            JogadorAtual = null;
        }
    }

    public void DarLance(int aumento)
    {
        if (JogadorAtual == null || JogadorAtual.Dinheiro < MaiorLance + aumento) return;

        int novoLance = MaiorLance + aumento;

        MaiorLance = novoLance;
        MaiorLicitante = JogadorAtual;

        indiceJogadorAtual++;
        AvancarTurno();
    }

    public void Desistir()
    {
        if (JogadorAtual == null) return;
        Participantes.Remove(JogadorAtual);
        AvancarTurno();
    }

    private void AvancarTurno()
    {
        if (Participantes.Count == 0)
        {
            FinalizarLeilao();
            return;
        }

        if (Participantes.Count == 1 && MaiorLicitante != null)
        {
            if (Participantes[0] == MaiorLicitante)
            {
                FinalizarLeilao();
                return;
            }
        }

        if (Participantes.Count == 1 && MaiorLicitante == null)
        {
            indiceJogadorAtual = 0;
            JogadorAtual = Participantes[indiceJogadorAtual];
            return;
        }

        indiceJogadorAtual = indiceJogadorAtual % Participantes.Count;
        JogadorAtual = Participantes[indiceJogadorAtual];
    }

    private void FinalizarLeilao()
    {
        JogadorAtual = null;
        if (MaiorLicitante != null)
        {
            Log.WriteLine($"Leilão finalizado! {MaiorLicitante} venceu com um lance de {MaiorLance}.");
        }
        else
        {
            Log.WriteLine("Leilão finalizado sem vencedores.");
        }
    }
}

//public void Executar()
//{

//    Jogador? proprietarioOriginal = PosseJogador.Proprietario;
//    List<Jogador> licitantesAtivos = new List<Jogador>(Participantes);

//    int index = (licitantesAtivos.IndexOf(JogadorIniciador) + 1) % licitantesAtivos.Count;

//    while (licitantesAtivos.Count > 1)
//    {
//        Jogador jogadorAtual = licitantesAtivos[index];

//        Log.WriteLine($"\nLance atual: ${LanceAtual} (por {MaiorLicitante?.Nome ?? "ninguém"})");

//        if (jogadorAtual.Dinheiro <= LanceAtual)
//        {
//            Log.WriteLine($"{jogadorAtual.Nome} não tem fundos para continuar e foi removido do leilão.");
//            licitantesAtivos.RemoveAt(index);
//            if (index >= licitantesAtivos.Count) index = 0;
//            continue;
//        }

//        bool turnoDoJogadorConcluido = false;
//        while (!turnoDoJogadorConcluido)
//        {
//            Console.Write($"{jogadorAtual.Nome} (Saldo: ${jogadorAtual.Dinheiro}), digite seu lance ou 'sair': ");
//            string? input = Console.ReadLine()?.Trim().ToLower();

//            if (input == "sair")
//            {
//                Log.WriteLine($"{jogadorAtual.Nome} saiu do leilão.");
//                licitantesAtivos.RemoveAt(index);
//                if (index >= licitantesAtivos.Count) index = 0;
//                turnoDoJogadorConcluido = true;
//            }
//            else if (int.TryParse(input, out int novoLance))
//            {
//                if (novoLance > LanceAtual && novoLance <= jogadorAtual.Dinheiro)
//                {
//                    LanceAtual = novoLance;
//                    MaiorLicitante = jogadorAtual;
//                    Log.WriteLine($"Novo lance de ${LanceAtual} por {jogadorAtual.Nome}!");
//                    turnoDoJogadorConcluido = true;
//                }
//                else if (novoLance <= LanceAtual)
//                {
//                    Log.WriteLine("Seu lance deve ser maior que o lance atual. Tente novamente.");
//                }
//                else
//                {
//                    Log.WriteLine("Você não tem dinheiro suficiente para este lance. Tente novamente.");
//                }
//            }
//            else
//            {
//                Log.WriteLine("Entrada inválida. Tente novamente.");
//            }
//        }

//        if (licitantesAtivos.Contains(jogadorAtual))
//        {
//            index = (index + 1) % licitantesAtivos.Count;
//        }
//    }

//    if (licitantesAtivos.Count == 1 && MaiorLicitante != null)
//    {
//        Jogador vencedor = MaiorLicitante;
//        Log.WriteLine($"{vencedor.Nome} venceu o leilão de {PosseJogador.Nome} por ${LanceAtual}!");

//        vencedor.Debitar(LanceAtual);
//        if (proprietarioOriginal != null)
//        {
//            Log.WriteLine($"O valor de ${LanceAtual} foi pago a {proprietarioOriginal.Nome}.");
//            proprietarioOriginal.Creditar(LanceAtual);
//            proprietarioOriginal.Posses.Remove(PosseJogador);
//        }
//        else
//        {
//            Log.WriteLine("O valor foi pago ao banco.");
//        }

//        PosseJogador.Proprietario = vencedor;
//        vencedor.AdicionarPosse(PosseJogador);
//    }
//    else
//    {
//        Log.WriteLine("O leilão terminou sem um vencedor. A PosseJogador permanece com seu dono original ou com o banco.");
//    }
//}
