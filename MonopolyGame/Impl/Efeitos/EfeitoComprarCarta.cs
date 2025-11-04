using MonopolyGame.Interface.Cartas;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;


public class EfeitoComprarCarta(IDeck deck) : IEfeitoJogador
{
    private readonly IDeck deck = deck;

    public void Aplicar(Jogador jogador)
    {
        if (jogador == null) throw new ArgumentNullException(nameof(jogador));

        ICarta? carta = deck.ComprarCarta();

        if (carta == null)
        {
            return;
        }

        carta.QuandoPegada(jogador);
    }
}
