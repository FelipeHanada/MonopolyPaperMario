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
        jogador.Partida.AdicionarRegistro($"Invertendo direção de {jogador.Nome}");
        Log.WriteLine(jogador.Reverso);
    }
}
