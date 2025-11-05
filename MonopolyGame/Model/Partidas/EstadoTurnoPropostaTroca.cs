using MonopolyGame.Interface;
using MonopolyGame.Interface.Partidas;
using MonopolyGame.Model.Leiloes;
using MonopolyGame.Model.PropostasTroca;

namespace MonopolyGame.Model.Partidas;


public class EstadoTurnoPropostaTroca(Jogador jogadorAtual, PropostaTroca propostaTroca) : AbstratoEstadoTurno
{
    public override EstadoTurnoId EstadoId { get; } = EstadoTurnoId.PropostaTroca;
    public override Jogador JogadorAtual { get; } = jogadorAtual;

    public override PropostaTroca PropostaTroca { get; } = propostaTroca;

    public IEstadoTurno EstadoTurnoAnterior { get; } = jogadorAtual.Partida.EstadoTurnoAtual;

    public override void EncerrarPropostaTroca(bool aceite)
    {
        if (aceite)
        {
            PropostaTroca.Efetuar();
        }
        
        // troca o estado do turno para o anterior
        JogadorAtual.Partida.EstadoTurnoAtual = EstadoTurnoAnterior;
    }
}

public class EstadoTurnoPropostaTrocaComLeilao(Jogador jogadorAtual, PropostaTroca propostaTroca, IPosseJogador posse)
    : EstadoTurnoPropostaTroca(jogadorAtual, propostaTroca)
{
    public override void EncerrarPropostaTroca(bool aceite)
    {
        if (aceite) base.EncerrarPropostaTroca(aceite);
        else
        {
            //JogadorAtual.Partida.IniciarLeilao(new Leilao(JogadorAtual.Partida, posse));
        }
    }
}
