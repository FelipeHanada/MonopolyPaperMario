using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;


internal class EfeitoReverterPodeJogar : IEfeitoJogador
{
    public void Aplicar(Jogador jogador)
    {
        jogador.PodeJogar = !jogador.PodeJogar;
    }
}
