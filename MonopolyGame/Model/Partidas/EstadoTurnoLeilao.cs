using MonopolyGame.Interface;
using MonopolyGame.Interface.Partidas;
using MonopolyGame.Model.Leiloes;

namespace MonopolyGame.Model.Partidas;


public class EstadoTurnoLeilao(Jogador jogadorAtual, IPosseJogador posseLeilaoda) : AbstratoEstadoTurno
{
   public override EstadoTurnoId EstadoId { get; } = EstadoTurnoId.Leilao;
   public override Jogador JogadorAtual { get; } = jogadorAtual;

   public override Leilao Leilao { get; } = new Leilao(jogadorAtual, posseLeilaoda);

   public override Jogador? JogadorAtualLeilao { get => Leilao.JogadorAtual; }

   public override void DarLanceLeilao(int aumento)
   {
       Leilao.DarLance(aumento);
   }

    public override void DesistirLeilao()
    {
        Leilao.Desistir();
    }
}
