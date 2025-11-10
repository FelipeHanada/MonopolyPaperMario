using MonopolyGame.Model.Cartas;

namespace MonopolyGame.Interface.Cartas;

enum CartasCofre
{
    CartaAntiGuys,
    CartaBandits,
    CartaChetRippo,
    CartaChuckQuizmo,
    CartaKetchnkoopa,
    CartaKoopaKoot,
    CartaMerlee,
    CartaMerluvelee,
    CartaMistake,
    CartaMistar,
    CartaPlayground,
    CartaRalph,
    CartaRipCheato,
    CartaShopping,
    CartaWhacka,
    CartaYoshiKids,
}


interface FabricaAbstrataPropriedade
{
    public CartaCofre CriaCarta(CartasCofre cartaId);
    public List<CartaCofre> CriaTodasAsCartas();
}
