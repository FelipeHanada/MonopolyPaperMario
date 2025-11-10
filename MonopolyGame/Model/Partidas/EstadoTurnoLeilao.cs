using MonopolyGame.Exceptions;
using MonopolyGame.Interface.Partidas;
using MonopolyGame.Interface.PosseJogador;
using MonopolyGame.Model.Leiloes;

namespace MonopolyGame.Model.Partidas;


public class EstadoTurnoLeilao(Jogador jogadorAtual, Leilao leilao) : AbstratoEstadoTurno
{
    public override EstadoTurnoId EstadoId { get; } = EstadoTurnoId.Leilao;
    public override Jogador JogadorAtual { get; } = jogadorAtual;

    public override Leilao Leilao { get; } = leilao;

   public override Jogador? JogadorAtualLeilao { get => Leilao.JogadorAtual; }

   public override bool DarLanceLeilao(int aumento)
   {
       return Leilao.DarLance(aumento);
   }

    public override bool DesistirLeilao()
    {
        if (!Leilao.Desistir()) return false;

        if (Leilao.Finalizado)
        {
            Leilao.MaiorLicitante?.AdicionarPosse(Leilao.PosseJogador);
            Leilao.MaiorLicitante?.Debitar(Leilao.MaiorLance);
            JogadorAtual.Partida.EstadoTurnoAtual = JogadorAtual.Partida.EstadoComumAtual;
        }

        return true;
    }
}
