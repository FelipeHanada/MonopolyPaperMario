using MonopolyGame.Utils;
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
        jogadorDaVez = partida.JogadorAtual;
        contadorDadosIguais = 0;

        if (jogadorDaVez == null) return;

        Log.WriteLine($"\n--- É a vez de {jogadorDaVez.Nome} ---");
        Log.WriteLine($"Saldo: ${jogadorDaVez.Dinheiro} | Posição: {partida.Tabuleiro?.GetPosicao(jogadorDaVez)}");
        //SE NÃO PODE JOGAR, RETORNA
        if (!jogadorDaVez.PodeJogar)
        {
            Log.WriteLine($"\n{jogadorDaVez.Nome} está proibido de jogar!");
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
                    Log.WriteLine("Estado de Leilão (não implementado).");
                    estadoAtual = estadoAnterior; // Restaura o estado anterior
                    break;
            }
        }
        FinalizarTurno();
    }

    private void ApresentarMenuFaseComum()
    {
        Log.WriteLine("\nEscolha uma ação:");
        if (estadoAtual == TurnoJogadorEstado.FaseComum)
        {
            Log.WriteLine("1. Rolar os dados");
            Log.WriteLine("2. Fazer uma proposta de troca");
            Log.WriteLine("3. Gerenciar propriedades - (Não implementado)");
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
                    Log.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    private void IniciarPropostaDeTroca()
    {
        if (partida == null || jogadorDaVez == null) return;

        var gerenciador = new GerenciadorDeTrocas(partida.Jogadores);
        gerenciador.IniciarNovaTroca(jogadorDaVez);
    }

    private void TentarSairDaPrisao()
    {
        if (jogadorDaVez == null || partida == null || partida.Tabuleiro == null) return;

        jogadorDaVez.IncrementarTurnosPreso();
        Log.WriteLine($"{jogadorDaVez.Nome} está na prisão. (Turno {jogadorDaVez.TurnosPreso})");

        if (jogadorDaVez.TurnosPreso >= 3)
        {
            Log.WriteLine("Terceiro turno na prisão! Você deve pagar $50 para sair.");
            try
            {
                jogadorDaVez.Debitar(50);
                Log.WriteLine("Fiança paga.");
                new EfeitoSairDaCadeia().Aplicar(jogadorDaVez);
                RolarDados();
            }
            catch (Exceptions.FundosInsuficientesException)
            {
                Log.WriteLine("Você não tem dinheiro para pagar a fiança e faliu!");
                jogadorDaVez.SetFalido(true);
                estadoAtual = TurnoJogadorEstado.FimDeTurno;
            }
        }
        else
        {
            Log.WriteLine("Tentando rolar dados iguais para sair...");

            int resultadoDado1 = dado.Next(1, 7);
            int resultadoDado2 = dado.Next(1, 7);
            Log.WriteLine($"Você rolou {resultadoDado1} e {resultadoDado2}.");

            if (resultadoDado1 == resultadoDado2)
            {
                Log.WriteLine("Dados iguais! Você está livre!");
                new EfeitoSairDaCadeia().Aplicar(jogadorDaVez);
                int totalDados = resultadoDado1 + resultadoDado2;
                partida.Tabuleiro.MoveJogador(jogadorDaVez, totalDados);
                estadoAtual = TurnoJogadorEstado.FaseComumDadoRolado;
            }
            else
            {
                Log.WriteLine("Você não rolou dados iguais e continua na prisão.");
                estadoAtual = TurnoJogadorEstado.FimDeTurno;
            }
        }
    }

    private void RolarDados()
    {
        if (jogadorDaVez == null || partida == null || partida.Tabuleiro == null) return;

        int resultadoDado1 = dado.Next(1, 7);
        int resultadoDado2 = dado.Next(1, 7);
        int totalDados = resultadoDado1 + resultadoDado2;
        bool dadosIguais = resultadoDado1 == resultadoDado2;

        Log.WriteLine($"{jogadorDaVez.Nome} rolou os dados e tirou {resultadoDado1} e {resultadoDado2}, totalizando {totalDados}.");

        if (dadosIguais)
        {
            contadorDadosIguais++;
            Log.WriteLine("Dados iguais!");

            if (contadorDadosIguais == 3)
            {
                Log.WriteLine("Três pares de dados iguais seguidos! Vá para a cadeia!");
                var efeitoCadeia = new EfeitoIrParaCadeia();
                efeitoCadeia.Aplicar(jogadorDaVez);
                estadoAtual = TurnoJogadorEstado.FimDeTurno;
            }
            else
            {
                partida.Tabuleiro.MoveJogador(jogadorDaVez, totalDados);
                Log.WriteLine("Jogue novamente!");
                estadoAtual = TurnoJogadorEstado.FaseComum;
            }
        }
        else
        {
            partida.Tabuleiro.MoveJogador(jogadorDaVez, totalDados);
            estadoAtual = TurnoJogadorEstado.FaseComumDadoRolado;
        }
    }

    //public void IniciarLeilao(IPosseJogador posseJogador)
    //{
    //    if (partida == null || jogadorDaVez == null) return;
    //    Log.WriteLine($"--- Leilão para {posseJogador.Nome} ---");
    //    var leilao = new Leilao(jogadorDaVez, posseJogador);
    //    leilao.Executar();
    //    FinalizarLeilao(leilao);
    //}

    private void FinalizarLeilao(Leilao leilao)
    {
        Log.WriteLine("Leilão finalizado.");
    }

    private void FinalizarTurno()
    {
        Log.WriteLine($"Fim do turno de {jogadorDaVez?.Nome}.");
    }
}
