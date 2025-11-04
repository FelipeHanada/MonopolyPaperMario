using MonopolyGame.Model.PossesJogador;
using MonopolyGame.Model.Leiloes;
using MonopolyGame.Impl.Efeitos;
using MonopolyGame.Interface;

namespace MonopolyGame.Model.Partidas;


public class TurnoJogador
{
    public enum TurnoJogadorEstado
    {
        FaseComum,
        FaseComumDadoRolado,
        PropostaTroca,
        Leilao,
        FimDeTurno
    }

    private static TurnoJogador? _instance;
    private readonly Random dado;
    private Partida? partida;
    private Jogador? jogadorDaVez;
    private int contadorDadosIguais;
    private TurnoJogadorEstado estadoAtual;
    private TurnoJogadorEstado estadoAnterior;

    private TurnoJogador()
    {
        dado = new Random();
        estadoAtual = TurnoJogadorEstado.FimDeTurno;
        estadoAnterior = TurnoJogadorEstado.FimDeTurno;
    }

    public static TurnoJogador Instance
    {
        get
        {
            _instance ??= new TurnoJogador();
            return _instance;
        }
    }

    public void IniciarTurno(Partida partida)
    {
        this.partida = partida;
        jogadorDaVez = partida.jogadorAtual;
        contadorDadosIguais = 0;

        if (jogadorDaVez == null) return;

        Console.WriteLine($"\n--- É a vez de {jogadorDaVez.Nome} ---");
        Console.WriteLine($"Saldo: ${jogadorDaVez.Dinheiro} | Posição: {partida.tabuleiro?.GetPosicao(jogadorDaVez)}");
        //SE NÃO PODE JOGAR, RETORNA
        if (!jogadorDaVez.PodeJogar)
        {
            Console.WriteLine($"\n{jogadorDaVez.Nome} está proibido de jogar!");
            FinalizarTurno();
            return;
        }
        if (jogadorDaVez.Preso)
        {
            TentarSairDaPrisao();
            // O loop principal só é iniciado se o jogador não estiver preso.
            // TentarSairDaPrisao já finaliza o turno ou o prepara para o próximo estado.
        }
        else
        {
            estadoAtual = TurnoJogadorEstado.FaseComum;
        }

        while (estadoAtual != TurnoJogadorEstado.FimDeTurno)
        {
            switch (estadoAtual)
            {
                case TurnoJogadorEstado.FaseComum:
                case TurnoJogadorEstado.FaseComumDadoRolado:
                    ApresentarMenuFaseComum();
                    break;
                case TurnoJogadorEstado.PropostaTroca:
                    IniciarPropostaDeTroca();
                    estadoAtual = estadoAnterior; // Restaura o estado anterior de forma simples
                    break;
                case TurnoJogadorEstado.Leilao:
                    Console.WriteLine("Estado de Leilão (não implementado).");
                    estadoAtual = estadoAnterior; // Restaura o estado anterior
                    break;
            }
        }
        FinalizarTurno();
    }

    private void ApresentarMenuFaseComum()
    {
        Console.WriteLine("\nEscolha uma ação:");
        if (estadoAtual == TurnoJogadorEstado.FaseComum)
        {
            Console.WriteLine("1. Rolar os dados");
            Console.WriteLine("2. Fazer uma proposta de troca");
            Console.WriteLine("3. Gerenciar propriedades - (Não implementado)");
            Console.Write("Opção: ");
            string? escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    RolarDados();
                    break;
                case "2":
                    estadoAnterior = estadoAtual;
                    estadoAtual = TurnoJogadorEstado.PropostaTroca;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("1. Fazer uma proposta de troca");
            Console.WriteLine("2. Gerenciar propriedades - (Não implementado)");
            Console.WriteLine("3. Finalizar turno");
            Console.Write("Opção: ");
            string? escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    estadoAnterior = estadoAtual;
                    estadoAtual = TurnoJogadorEstado.PropostaTroca;
                    break;
                case "3":
                    estadoAtual = TurnoJogadorEstado.FimDeTurno;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    private void IniciarPropostaDeTroca()
    {
        if (partida == null || jogadorDaVez == null) return;

        var gerenciador = new GerenciadorDeTrocas(partida.jogadores);
        gerenciador.IniciarNovaTroca(jogadorDaVez);
    }

    private void TentarSairDaPrisao()
    {
        if (jogadorDaVez == null || partida == null || partida.tabuleiro == null) return;

        jogadorDaVez.IncrementarTurnosPreso();
        Console.WriteLine($"{jogadorDaVez.Nome} está na prisão. (Turno {jogadorDaVez.TurnosPreso})");

        if (jogadorDaVez.TurnosPreso >= 3)
        {
            Console.WriteLine("Terceiro turno na prisão! Você deve pagar $50 para sair.");
            try
            {
                jogadorDaVez.Debitar(50);
                Console.WriteLine("Fiança paga.");
                new EfeitoSairDaCadeia(partida).Aplicar(jogadorDaVez);
                RolarDados();
            }
            catch (Exceptions.FundosInsuficientesException)
            {
                Console.WriteLine("Você não tem dinheiro para pagar a fiança e faliu!");
                jogadorDaVez.SetFalido(true);
                estadoAtual = TurnoJogadorEstado.FimDeTurno;
            }
        }
        else
        {
            Console.WriteLine("Tentando rolar dados iguais para sair...");

            int resultadoDado1 = dado.Next(1, 7);
            int resultadoDado2 = dado.Next(1, 7);
            Console.WriteLine($"Você rolou {resultadoDado1} e {resultadoDado2}.");

            if (resultadoDado1 == resultadoDado2)
            {
                Console.WriteLine("Dados iguais! Você está livre!");
                new EfeitoSairDaCadeia(partida).Aplicar(jogadorDaVez);
                int totalDados = resultadoDado1 + resultadoDado2;
                partida.tabuleiro.MoveJogador(jogadorDaVez, totalDados);
                estadoAtual = TurnoJogadorEstado.FaseComumDadoRolado;
            }
            else
            {
                Console.WriteLine("Você não rolou dados iguais e continua na prisão.");
                estadoAtual = TurnoJogadorEstado.FimDeTurno;
            }
        }
    }

    private void RolarDados()
    {
        if (jogadorDaVez == null || partida == null || partida.tabuleiro == null) return;

        int resultadoDado1 = dado.Next(1, 7);
        int resultadoDado2 = dado.Next(1, 7);
        int totalDados = resultadoDado1 + resultadoDado2;
        bool dadosIguais = resultadoDado1 == resultadoDado2;

        Console.WriteLine($"{jogadorDaVez.Nome} rolou os dados e tirou {resultadoDado1} e {resultadoDado2}, totalizando {totalDados}.");

        if (dadosIguais)
        {
            contadorDadosIguais++;
            Console.WriteLine("Dados iguais!");

            if (contadorDadosIguais == 3)
            {
                Console.WriteLine("Três pares de dados iguais seguidos! Vá para a cadeia!");
                var efeitoCadeia = new EfeitoIrParaCadeia(partida);
                efeitoCadeia.Aplicar(jogadorDaVez);
                estadoAtual = TurnoJogadorEstado.FimDeTurno;
            }
            else
            {
                partida.tabuleiro.MoveJogador(jogadorDaVez, totalDados);
                Console.WriteLine("Jogue novamente!");
                estadoAtual = TurnoJogadorEstado.FaseComum;
            }
        }
        else
        {
            partida.tabuleiro.MoveJogador(jogadorDaVez, totalDados);
            estadoAtual = TurnoJogadorEstado.FaseComumDadoRolado;
        }
    }

    public void IniciarLeilao(IPosseJogador posseJogador)
    {
        if (partida == null || jogadorDaVez == null) return;
        Console.WriteLine($"--- Leilão para {posseJogador.Nome} ---");
        var leilao = new Leilao(partida, posseJogador, partida.jogadores.Where(j => !j.Falido).ToList(), jogadorDaVez);
        leilao.Executar();
        FinalizarLeilao(leilao);
    }

    private void FinalizarLeilao(Leilao leilao)
    {
        Console.WriteLine("Leilão finalizado.");
    }

    private void FinalizarTurno()
    {
        Console.WriteLine($"Fim do turno de {jogadorDaVez?.Nome}.");
    }
}
