using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;

namespace MonopolyGame.Impl.Efeitos;


internal class EfeitoAplicarDesconto(int percentual) : IEfeitoJogador
{
    private readonly int percentual = percentual;

    public void Aplicar(Jogador jogador)
    {
        jogador.Desconto = percentual;
    }
}
