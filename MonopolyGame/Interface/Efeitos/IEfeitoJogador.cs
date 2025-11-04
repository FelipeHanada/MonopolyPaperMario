using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Interface.Efeitos;


public interface IEfeitoJogador
{
    Partida GetPartida();
    void Aplicar(Jogador jogador);
}
