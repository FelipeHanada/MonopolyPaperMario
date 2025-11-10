using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;


class EfeitoReverterComprarJogador : IEfeitoJogador
{
    public void Aplicar(Jogador jogador)
    {
        if (jogador == null) throw new ArgumentNullException(nameof(jogador));
        jogador.PodeComprar = !jogador.PodeComprar;
    }
}
