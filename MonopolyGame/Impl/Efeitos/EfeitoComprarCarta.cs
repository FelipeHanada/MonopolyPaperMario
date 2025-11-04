using MonopolyGame.Interface.Cartas;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;


public class EfeitoComprarCarta(Partida partida, IDeck deck) : EfeitoJogador(partida)
{
    private readonly IDeck deck = deck;

    public override void Aplicar(Jogador jogador)
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
