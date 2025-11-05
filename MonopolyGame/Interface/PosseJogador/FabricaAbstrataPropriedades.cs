using MonopolyGame.Model.Cartas;

namespace MonopolyGame.Interface.Cartas;

enum Imoveis
{
}


interface FabricaAbstrataPropriedades
{
    public CartaCofre CriaCarta(CartasCofre cartaId);
    public List<CartaCofre> CriaTodasAsCartas();
}
