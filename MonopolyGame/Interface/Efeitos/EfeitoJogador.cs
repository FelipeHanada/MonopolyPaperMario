using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Interface.Efeitos;


public abstract class EfeitoJogador(Partida partida) : IEfeitoJogador
{
    private readonly Partida partida = partida;

    public Partida GetPartida()
    {
        return partida;
    }

    public abstract void Aplicar(Jogador jogador);
}
