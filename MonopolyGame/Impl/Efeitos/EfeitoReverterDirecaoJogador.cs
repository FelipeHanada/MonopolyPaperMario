using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;


internal class EfeitoReverterDirecaoJogador : IEfeitoJogador
{
    public void Aplicar(Jogador jogador)
    {
        Console.WriteLine("Revertendo direção do jogador");
        jogador.Reverso = !jogador.Reverso;
        Console.WriteLine(jogador.Reverso);
    }
}
