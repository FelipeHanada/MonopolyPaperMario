using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Interface.Cartas;


public interface ICarta
{
    string GetDescricao();
    void QuandoPegada(Jogador jogador);
}
