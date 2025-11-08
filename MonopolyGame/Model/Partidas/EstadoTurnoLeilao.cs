using MonopolyGame.Interface;
using MonopolyGame.Interface.Partidas;
using MonopolyGame.Model.Leiloes;

namespace MonopolyGame.Model.Partidas;


public class TurnoLeilao(Jogador jogadorAtual, IPosseJogador posseLeilaoda) : AbstractEstadoTurno
{
   public override EstadoTurnoId EstadoId { get; } = EstadoTurnoId.Leilao;
   public override Jogador JogadorAtual { get; } = jogadorAtual;

   public Leilao LeilaoAtual { get; } = new Leilao(jogadorAtual, posseLeilaoda);

   public Jogador? JogadorAtualLeilao { get => LeilaoAtual.JogadorAtual; }

   public override void DarLanceLeilao(int aumento)
   {
       LeilaoAtual.DarLance(aumento);
   }

    public override void DesistirLeilao()
    {
        LeilaoAtual.Desistir();
    }
}
