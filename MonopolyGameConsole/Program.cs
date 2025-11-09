using MonopolyGame.Interface;
using MonopolyGame.Interface.Partidas;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PropostasTroca;

namespace MonopolyGameConsole;

class Program
{
    // A instância da Partida é estática e somente leitura, o que é bom.
    private static readonly Partida partida = new(["Mario", "Luigi", "Peach", "Yoshi"]);

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // Garante que emojis sejam exibidos corretamente
        Console.WriteLine("🎲 Monopoly Paper Mario! 🎲");

        ApresentarJogadores();

        // Loop principal do jogo
        while (partida.JogadoresAtivos.Count > 1)
        {
            IniciarTurno();
        }

        ExibirVencedor();
    }

    /// <summary>
    /// Apresenta a lista de jogadores no início do jogo.
    /// </summary>
    private static void ApresentarJogadores()
    {
        Console.WriteLine("Jogadores:");
        foreach (var jogador in partida.Jogadores)
        {
            Console.WriteLine($"- {jogador.Nome} (Saldo Inicial: ${jogador.Dinheiro})");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Exibe o vencedor ao final do jogo.
    /// </summary>
    private static void ExibirVencedor()
    {
        // Encontra o primeiro jogador que não faliu (Assumindo que `Falido` é a condição de derrota)
        var vencedor = partida.Jogadores.FirstOrDefault(j => !j.Falido);
        if (vencedor != null)
        {
            Console.WriteLine($"🎉 Fim de jogo! O vencedor é {vencedor.Nome}! 🎉");
        }
        else
        {
            Console.WriteLine("😢 Fim de jogo! Nenhum vencedor claro (Todos faliram simultaneamente?).");
        }
    }

    // --------------------------------------------------------------------------------
    // Lógica do Turno
    // --------------------------------------------------------------------------------

    public static void IniciarTurno()
    {
        var jogadorAtual = partida.JogadorAtual;
        var posicaoAtual = partida.Tabuleiro?.GetPosicao(jogadorAtual);
        var nomeCasa = partida.Tabuleiro?.Pisos[posicaoAtual ?? 0].Nome ?? "Casa Desconhecida";

        Console.WriteLine($"--- É a vez de {jogadorAtual.Nome} ---");
        Console.WriteLine($"💰 Saldo: ${jogadorAtual.Dinheiro:N0} | 📍 Posição: {posicaoAtual}: {nomeCasa}");

        if (!jogadorAtual.PodeJogar)
        {
            Console.WriteLine($"{jogadorAtual.Nome} está proibido de jogar!");
            partida.FinalizarTurno();
            return;
        }

        while (jogadorAtual == partida.JogadorAtual)
        {
            switch (partida.EstadoTurnoAtual.EstadoId)
            {
                case EstadoTurnoId.Comum:
                    ApresentarMenuFaseComum();
                    break;
                case EstadoTurnoId.PropostaTroca:
                    ApresentarMenuFasePropostaTroca();
                    break;
                case EstadoTurnoId.Leilao:
                    ApresentarMenuFaseLeilao();
                    break;
                default:
                    Console.WriteLine($"Estado de Turno Não Reconhecido: {partida.EstadoTurnoAtual.EstadoId}. Finalizando turno.");
                    partida.FinalizarTurno();
                    break;
            }
        }
    }

    private static void ApresentarMenuFaseComum()
    {
        Console.WriteLine();
        Console.WriteLine("--------------- [ FASE COMUM ] ---------------");
        Console.WriteLine("Escolha uma ação:");
        Console.WriteLine($"1. Rolar os dados {(partida.EstadoTurnoAtual.PodeRolarDados ? "" : "(bloqueado)")}");
        Console.WriteLine("2. Gerenciar propriedades (Hipotecar/Construir) - (Não implementado)");
        Console.WriteLine("3. Fazer uma proposta de trocas");
        Console.WriteLine($"4. Finalizar Turno {(partida.EstadoTurnoAtual.PodeEncerrarTurno ? "" : "(bloqueado)")}");

        var acoes = new Dictionary<string, Action>
        {
            ["1"] = () => { RolarDados(); },
            ["2"] = () => { GerenciarPropriedades(); },
            ["3"] = () => { FazerPropostaTroca(); },
            ["4"] = () => { partida.FinalizarTurno(); }
        };

        var escolhasDisponiveis = acoes.Keys.Where(k =>
            (k == "1" && partida.EstadoTurnoAtual.PodeRolarDados) ||
            (k == "4" && partida.EstadoTurnoAtual.PodeEncerrarTurno) ||
            (k == "2" || k == "3") // Opções de "Não implementado" geralmente ficam sempre disponíveis
        ).ToHashSet();

        if (!partida.EstadoTurnoAtual.PodeRolarDados) escolhasDisponiveis.Remove("1");
        if (!partida.EstadoTurnoAtual.PodeEncerrarTurno) escolhasDisponiveis.Remove("4");


        var escolha = LerEscolhaUsuario(escolhasDisponiveis);

        if (acoes.TryGetValue(escolha, out var acao))
        {
            acao.Invoke(); // Executa a ação
        }
    }

    private static void GerenciarPropriedades()
    {
        Console.WriteLine("🏗️ Gerenciar Propriedades (Não implementado). Retornando ao menu principal.");
    }

    private static void FazerPropostaTroca()
    {
        Console.WriteLine("--------------- [ MONTANDO PROPOSTA DE TROCA ] ---------------");

        var jogadores = partida.Jogadores
            .Select((jogador, indice) => new { Jogador = jogador, Indice = (indice + 1).ToString() })
            .ToDictionary(item => item.Indice, item => item.Jogador);
        var escolhasDisponiveis = new HashSet<string>(jogadores.Keys);
        
        foreach (var (op, jogador) in jogadores)
        {
            Console.Write($"{op}. {jogador.Nome}");
            if (jogador == partida.JogadorAtual)
            {
                Console.Write(" (jogador atual)");
                escolhasDisponiveis.Remove(op);
            }
            else if (jogador.Falido)
            {
                Console.Write(" (jogador falido)");
                escolhasDisponiveis.Remove(op);
            }
            Console.WriteLine();

            if (jogador == partida.JogadorAtual) continue;
        }

        if (escolhasDisponiveis.Count == 0)
        {
            Console.WriteLine("Nenhum outro jogador elegível para troca.");
            return;
        }

        var escolha = LerEscolhaUsuario(escolhasDisponiveis);
        Jogador ofertante = partida.JogadorAtual,
                destinatario = jogadores[escolha];
        PropostaTroca propostaTroca = new(ofertante, destinatario);

        // loop para adicionar itens na proposta
        bool montandoProposta = true;
        while (montandoProposta)
        {
            Console.WriteLine("1. Adicionar posse de " + ofertante.Nome);
            Console.WriteLine("2. Adicionar posse de " + destinatario.Nome);
            Console.WriteLine("3. Adicionar dinheiro de " + ofertante.Nome);
            Console.WriteLine("4. Adicionar dinheiro de " + destinatario.Nome);
            Console.WriteLine("5. Encerrar");

            escolha = LerEscolhaUsuario(["1", "2", "3", "4", "5"]);
            switch (escolha)
            {
                case "1":
                case "2":
                    var posses = (escolha == "1" ? ofertante : destinatario)
                        .Posses
                        .Select((posse, indice) => new { Posse = posse, Indice = (indice + 1).ToString() })
                        .ToDictionary(item => item.Indice, item => item.Posse);

                    Console.WriteLine("Escolha uma posse para adicionar à proposta:");
                    foreach (var (op, posse) in posses)
                    {
                        Console.WriteLine($"{op}. {posse.Nome}");
                    }
                    var posseEscolhida = LerEscolhaUsuario([.. posses.Keys]);

                    if (escolha == "1")
                    {
                        propostaTroca.PossesOfertadas.Add(posses[posseEscolhida]);
                    } else
                    {
                        propostaTroca.PossesDesejadas.Add(posses[posseEscolhida]);
                    }
                    Console.WriteLine("Posse " + posses[posseEscolhida].Nome + " adicionada à proposta.");

                    break;

                case "3":
                case "4":
                    Console.WriteLine("Digite o valor em dinheiro para adicionar à proposta:");
                    string? valorInput = Console.ReadLine();
                    if (valorInput == null)
                        {
                        Console.WriteLine("Valor inválido. Tente novamente.");
                        break;
                    }
                    if (!int.TryParse(valorInput, out int valor) || valor < 0)
                    {
                        Console.WriteLine("Valor inválido. Tente novamente.");
                        break;
                    }
                    if (escolha == "3")
                    {
                        propostaTroca.DinheiroOfertado += valor;
                    }
                    else
                    {
                        propostaTroca.DinheiroOfertado -= valor;
                    }
                    break;

                case "5":
                    montandoProposta = false;
                    break;
            }
        }

        partida.IniciarPropostaTroca(propostaTroca);
    }

    private static void ApresentarMenuFasePropostaTroca()
    {
        var propostaTroca = partida.EstadoTurnoAtual.PropostaTroca;
        var destinatario = propostaTroca.Destinatario;
        var ofertante = propostaTroca.Ofertante;

        Console.WriteLine("--------------- [ FASE PROPOSTA TROCA ] ---------------");
        Console.WriteLine($"Proposta de Troca para {destinatario.Nome}:");

        // Detalhes da Oferta (o que o Destinatário RECEBE)
        Console.WriteLine($"🎁 Posses que {destinatario.Nome} irá receber:");
        if (propostaTroca.PossesOfertadas.Count > 0)
        {
            foreach (var posse in propostaTroca.PossesOfertadas)
            {
                Console.WriteLine($"- {posse.Nome}");
            }
        }
        else
        {
            Console.WriteLine("- Nenhuma posse");
        }

        if (ofertante != null)
        {
            Console.WriteLine($"💰 Posses que {ofertante.Nome} irá receber:");
            if (propostaTroca.PossesDesejadas.Count > 0)
            {
                foreach (var posse in propostaTroca.PossesDesejadas)
                {
                    Console.WriteLine($"- {posse.Nome}");
                }
            }
            else
            {
                Console.WriteLine("- Nenhuma posse");
            }
        }

        if (propostaTroca.DinheiroOfertado != 0)
        {
            var valorAbsoluto = Math.Abs(propostaTroca.DinheiroOfertado);
            if (propostaTroca.DinheiroOfertado > 0)
            {
                Console.WriteLine($"💸 {destinatario.Nome} também irá receber ${valorAbsoluto:N0}.");
            }
            else
            {
                Console.WriteLine($"💸 {destinatario.Nome} terá que pagar ${valorAbsoluto:N0}.");
            }
        }
        else
        {
            Console.WriteLine("💸 Nenhuma transação em dinheiro.");
        }

        Console.WriteLine("Escolha uma ação:");
        Console.WriteLine("1. Aceitar a Proposta");
        Console.WriteLine("2. Recusar a Proposta");

        var escolha = LerEscolhaUsuario(["1", "2"]);

        partida.EncerrarPropostaTroca(escolha == "1");
        Console.WriteLine(escolha == "1" ? "✅ Proposta Aceita!" : "❌ Proposta Recusada!");
    }

    private static void ApresentarMenuFaseLeilao()
    {
        Console.WriteLine("🔨 Estado de Leilão (não implementado). Retornando ao menu principal.");
        partida.FinalizarTurno();
    }

    private static void RolarDados()
    {
        var jogadorAtual = partida.JogadorAtual;
        var presoAntes = jogadorAtual.Preso;

        partida.EstadoTurnoAtual.RolarDados(out (int dado1, int dado2) dados, out int posicaoFinal);
        int totalDados = dados.dado1 + dados.dado2;

        Console.WriteLine($"🎲 {jogadorAtual.Nome} rolou os dados e tirou {dados.dado1} e {dados.dado2}, totalizando {totalDados}.");

        if (dados.dado1 == dados.dado2)
        {
            Console.WriteLine("✨ Dados iguais! O jogador pode rolar novamente após a ação.");
        }

        if (presoAntes)
        {
            if (!jogadorAtual.Preso)
            {
                Console.WriteLine("🥳 O jogador foi solto! (por tirar dados iguais ou usar um cartão).");
            }
            else
            {
                Console.WriteLine("😞 Que pena, o jogador não tirou dados iguais, portanto, ainda está na cadeia.");
            }
        }
        else
        {
            var nomeCasa = partida.Tabuleiro?.Pisos[posicaoFinal].Nome ?? "Casa Desconhecida";
            Console.WriteLine($"{jogadorAtual.Nome} moveu-se para a casa {posicaoFinal}: '{nomeCasa}'");
        }
    }

    private static string LerEscolhaUsuario(HashSet<string> escolhasValidas)
    {
        string? escolhaInput;
        string escolha;
        string opcoes = string.Join(", ", escolhasValidas.OrderBy(s => s));

        while (true)
        {
            Console.Write($"Opção ({opcoes}): ");
            escolhaInput = Console.ReadLine();

            if (escolhaInput != null && escolhasValidas.Contains(escolhaInput.Trim()))
            {
                escolha = escolhaInput.Trim();
                break;
            }
            Console.WriteLine("⚠️ Esta escolha não está disponível! Tente novamente.");
        }

        return escolha;
    }
}