using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;


internal class EfeitoReverterPodeJogar(Partida partida) : EfeitoJogador(partida)
{
    public override void Aplicar(Jogador jogador)
    {
        jogador.PodeJogar = !jogador.PodeJogar;
    }
}
