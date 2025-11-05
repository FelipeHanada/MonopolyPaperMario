using MonopolyGame.Interface;
using MonopolyGame.Interface.Partidas;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PropostasTroca;

namespace MonopolyGameConsole;


class Program
{
    private static readonly Partida partida = new(["Mario", "Luigi", "Peach", "Yoshi"]);

    static void Main(string[] args)
    {
        Console.WriteLine("🎲 Monopoly Paper Mario! 🎲");

        Console.WriteLine("Jogadores:");
        foreach (Jogador jogador in partida.Jogadores)
        {
            Console.WriteLine("- " + jogador.Nome);
        }
        Console.WriteLine();

        // Loop principal do jogo
        while (partida.JogadoresAtivos.Count > 1)
        {
            IniciarTurno();
        }

        var vencedor = partida.Jogadores.FirstOrDefault(j => !j.Falido);
        if (vencedor != null)
        {
            Console.WriteLine($"\n🎉 Fim de jogo! O vencedor é {vencedor.Nome}! 🎉");
        }
    }

    public static void IniciarTurno()
    {
        Console.WriteLine($"\n--- É a vez de {partida.JogadorAtual.Nome} ---");
        Console.WriteLine($"Saldo: ${partida.JogadorAtual.Dinheiro} | Posição: {partida.Tabuleiro?.GetPosicao(partida.JogadorAtual)}");
        //SE NÃO PODE JOGAR, RETORNA
        if (!partida.JogadorAtual.PodeJogar)
        {
            Console.WriteLine($"\n{partida.JogadorAtual.Nome} está proibido de jogar!");
            partida.FinalizarTurno();
            return;
        }

        Jogador jogadorAtual = partida.JogadorAtual;
        while (jogadorAtual == partida.JogadorAtual)
        {
            switch (partida.EstadoTurnoAtual.EstadoId)
            {
                case EstadoTurnoId.Comum:
                    ApresentarMenuFaseComum();
                    break;
                case EstadoTurnoId.PropostaTroca:
                    ApresentarMenuFasePropostaVenda();
                    break;
                case EstadoTurnoId.Leilao:
                    //ApresentarMenuFaseLeilao();
                    Console.WriteLine("Estado de Leilão (não implementado).");
                    break;
            }
        }
    }

    private static void ApresentarMenuFaseComum()
    {
        Console.WriteLine("--------------- [ FASE COMUM ] ---------------");
        Console.WriteLine("Escolha uma ação:");
        Console.WriteLine("1. Rolar os dados" + (partida.EstadoTurnoAtual.PodeRolarDados ? "" : " (bloqueado)"));
        Console.WriteLine("2. Fazer uma proposta de troca");
        Console.WriteLine("3. Gerenciar propriedades - (Não implementado)");
        Console.WriteLine("4. Finalizar Turno" + (partida.EstadoTurnoAtual.PodeEncerrarTurno ? "" : "(bloqueado)"));

        HashSet<string> escolhasDisponiveis = ["1", "2", "3", "4"];
        if (!partida.EstadoTurnoAtual.PodeRolarDados) escolhasDisponiveis.Remove("1");
        if (!partida.EstadoTurnoAtual.PodeEncerrarTurno) escolhasDisponiveis.Remove("4");

        string escolha;
        while (true)
        {
            Console.Write("Opção: ");
            string? escolhaInput = Console.ReadLine();
            if (escolhaInput != null && escolhasDisponiveis.Contains(escolhaInput))
            {
                escolha = escolhaInput;
                break;
            }
            Console.WriteLine("Esta escolha não está disponível!");
        }

        switch (escolha)
        {
            case "1":
                RolarDados();
                break;
            case "2":
                break;
            case "4":
                partida.FinalizarTurno();
                break;
        }
    }

    private static void ApresentarMenuFasePropostaVenda()
    {
        PropostaTroca propostaTroca = partida.EstadoTurnoAtual.PropostaTroca;
        Console.WriteLine("--------------- [ FASE PROPOSTA TROCA ] ---------------");
        Console.WriteLine("Posses que " + propostaTroca.Destinatario.Nome + " irá receber: ");
        foreach (IPosseJogador posse in propostaTroca.PossesOfertadas)
        {
            Console.WriteLine("- " + posse.Nome);
        }
        if (propostaTroca.Ofertante != null)
        {
            Console.WriteLine("Posses que " + propostaTroca.Ofertante.Nome + " irá receber: ");
            foreach (IPosseJogador posse in propostaTroca.PossesDesejadas)
            {
                Console.WriteLine("- " + posse.Nome);
            }
        }
        if (propostaTroca.DinheiroOfertado != 0)
        {
            Console.Write(propostaTroca.Destinatario.Nome + " ");
            if (propostaTroca.DinheiroOfertado > 0)
            {
                Console.Write("irá receber ");
            } else
            {
                Console.Write("terá que pagar ");
            }
            Console.WriteLine(Math.Abs(propostaTroca.DinheiroOfertado));
        }


        Console.WriteLine("Escolha uma ação:");
        Console.WriteLine("1. Aceitar");
        Console.WriteLine("2. Recusar");

        string escolha;
        while (true)
        {
            Console.Write("Opção: ");
            string? escolhaInput = Console.ReadLine();
            if (escolhaInput == "1" || escolhaInput == "2")
            {
                escolha = escolhaInput;
                break;
            }
            Console.WriteLine("Esta escolha não está disponível!");
        }

        partida.EncerrarPropostaTroca((escolha == "1"));
    }

    private static void RolarDados()
    {
        bool preso = partida.JogadorAtual.Preso;
        partida.RolarDados(out (int, int) dados, out int posicaoFinal);
        int totalDados = dados.Item1 + dados.Item2;
        Console.WriteLine($"{partida.JogadorAtual.Nome} rolou os dados e tirou {dados.Item1} e {dados.Item2}, totalizando {totalDados}.");
        if (dados.Item1 == dados.Item2) Console.WriteLine("Dados iguais!");

        if (preso)
        {
            if (!partida.JogadorAtual.Preso)
            {
                Console.WriteLine("O jogador foi solto!");
            } else
            {
                Console.WriteLine("Que pena, o jogador não tirou dados iguais, portanto, ainda está na cadeia.");
            }
        } else
        {
            Console.WriteLine(partida.JogadorAtual.Nome + " moveu-se para a casa " + posicaoFinal + ": '" + partida.Tabuleiro.Pisos[posicaoFinal].Nome + "'");
        }
    }

    //static void Main(string[] args)
    //{
    //    Console.WriteLine("🎲 Monopoly Paper Mario! 🎲");

    //    Console.WriteLine("Jogadores:");
    //    foreach (Jogador jogador in partida.Jogadores)
    //    {
    //        Console.WriteLine("- " + jogador.Nome);
    //    }
    //    Console.WriteLine();

    //    // Loop principal do jogo
    //    while (partida.JogadoresAtivos.Count() > 1)
    //    {
    //        partida.ProximoTurno(); // Apenas avança o ponteiro do jogador atual

    //        TurnoJogador.Instance.IniciarTurno(partida);
    //    }

    //    var vencedor = partida.Jogadores.FirstOrDefault(j => !j.Falido);
    //    if (vencedor != null)
    //    {
    //        Console.WriteLine($"\n🎉 Fim de jogo! O vencedor é {vencedor.Nome}! 🎉");
    //    }
    //}

    //static void IniciarTurno()
    //{
    //    Console.WriteLine()
    //}
}
