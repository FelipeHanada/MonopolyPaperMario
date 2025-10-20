using System;
using System.Dynamic;
using MonpolyMario.Components.Game.Exceptions;
using MonpolyMario.Components.Game.Model;

public enum TurnoJogadorEstado
{
    Comum,
    PropostaVenda,
    Leilão,
}

public class TurnoJogador
{
    private static Random random = new Random();
    private static TurnoJogador? _instancia = null;
    private static Partida partida;
    private static Jogador jogador;
    private static Leilao? leilao;
    private static PropostaVenda? propostaVenda;
    private static TurnoJogadorEstado estado;
    private static bool dadosRolados = false;

    private TurnoJogador(Partida partida)
    {
        partida = partida;
        estado = TurnoJogadorEstado.Comum;

    }

    public static TurnoJogador GetInstancia()
    {
        if (_instancia == null)
        {
            _instancia = new TurnoJogador(partida);
        }

        return _instancia;
    }

    public static (int, int, int) RolarDados()
    {
        if (estado == TurnoJogadorEstado.Comum)
        {
            int dado1 = random.Next(1, 7);
            int dado2 = random.Next(1, 7);

            int casasParaAndar = dado1 + dado2;

            dadosRolados = true;

            return (dado1, dado2, casasParaAndar);
        }
        else
        {
            throw new EstadoInvalidoException();
        }
    }


    public static void ComecarPropostaVenda()
    {
        if (estado == TurnoJogadorEstado.Comum)
        {
            estado = TurnoJogadorEstado.PropostaVenda;

            Jogador[] jogadores = partida.getjogadores();


            Console.Write("Informe o jogador para o qual deseja fazer a proposta: ");
            string nomeProcurado = Console.ReadLine();

            Jogador jogadorRequisitado = null;

            foreach (Jogador jogador in jogadores)
            {
                if (jogador.getNome().Equals(nomeProcurado, StringComparison.OrdinalIgnoreCase))
                {
                    jogadorRequisitado = jogador;
                    break;
                }
            }

            Console.Write("Qual o valor oferecido: ");
            string valorString = Console.ReadLine();

            int valor = int.Parse(valorString);

            Console.Write("Qual posse deseja adiquirir: ");
            string nomePosse = Console.ReadLine();

            PosseJogador[] posses = jogador.getPosses();

            PosseJogador posse = null;

            foreach (PosseJogador posseJogador in posses)
            {
                if (posse.getNome().Equals(nomePosse, StringComparison.OrdinalIgnoreCase))
                {
                    posse = posseJogador;
                    break;
                }
            }

            PropostaVenda propostaVenda = new PropostaVenda(valor, posse, jogadorRequisitado);

            bool aceito = propostaVenda.SerAceito();

            if (aceito == true)
            {
                jogador.transferirDinheiroPara(jogadorRequisitado, valor);
                jogadorRequisitado.transferirPossePara(jogador, posse);

            }
            else
            {
                estado = TurnoJogadorEstado.Comum;
                return;
            }

            estado = TurnoJogadorEstado.Comum;
        }
        else
        {
            throw new EstadoInvalidoException();
        }

    }

    public static void ComecarLeilao(PosseJogador posse)
    {
        if (estado == TurnoJogadorEstado.Comum)
        {
            estado = TurnoJogadorEstado.Leilão;

            Leilao leilao = new Leilao(posse, jogador);
            leilao.ExecutarLeilao();

            estado = TurnoJogadorEstado.Comum;
        }
        else
        {
            throw new EstadoInvalidoException();
        }
    }

    public static TurnoJogador FinalizaTurno()
    {
        if (estado == TurnoJogadorEstado.Comum)
        {
            TurnoJogador proximoTurno = TurnoJogador.GetInstancia();
            int turnoAtual = partida.getTurno();

            partida.setTurno((turnoAtual + 1) % 5);

            return proximoTurno;
        }
        else
        {
            throw new EstadoInvalidoException();
        }
    }

}