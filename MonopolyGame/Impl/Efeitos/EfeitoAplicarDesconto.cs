using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;

namespace MonopolyGame.Impl.Efeitos;


internal class EfeitoAplicarDesconto(Partida partida, int percentual) : EfeitoJogador(partida)
{
    private int percentual = percentual;

    public override void Aplicar(Jogador jogador)
    {
        jogador.Desconto = percentual;
    }
}
