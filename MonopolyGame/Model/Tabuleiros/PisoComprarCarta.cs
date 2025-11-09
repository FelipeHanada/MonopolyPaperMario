using MonopolyGame.Impl.Efeitos;
using MonopolyGame.Interface.Cartas;

namespace MonopolyGame.Model.Tabuleiros;

public class PisoComprarCartaSorte(string nome, IDeck deck) : Piso(nome, new EfeitoComprarCarta(deck))
{
    public IDeck Deck { get; } = deck;
}


public class PisoComprarCartaCofre(string nome, IDeck deck) : Piso(nome, new EfeitoComprarCarta(deck))
{
    public IDeck Deck { get; } = deck;
}
