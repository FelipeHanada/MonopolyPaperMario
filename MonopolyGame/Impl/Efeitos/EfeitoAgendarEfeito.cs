using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;

namespace MonopolyGame.Impl.Efeitos;


internal class EfeitoAgendarEfeito(Partida partida, IEfeitoJogador efeito, int delta) : EfeitoJogador(partida)
{
    private readonly IEfeitoJogador efeito = efeito;
    private readonly int delta = delta;
    
    public override void Aplicar(Jogador jogador)
    {
        GetPartida().addEfeitoTurnoParaJogadores(delta, efeito, [jogador]);
    }
}
