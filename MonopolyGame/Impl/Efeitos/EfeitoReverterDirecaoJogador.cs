using MonopolyGame.Utils;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;


internal class EfeitoReverterDirecaoJogador : IEfeitoJogador
{
    public void Aplicar(Jogador jogador)
    {
        Log.WriteLine("Revertendo direção do jogador");
        jogador.Reverso = !jogador.Reverso;
        Log.WriteLine(jogador.Reverso);
    }
}
