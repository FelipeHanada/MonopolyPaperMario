using MonopolyGame.Interface.Efeitos;

namespace MonopolyGame.Model.Cartas;


public class CartaCofre(string descricao, IEfeitoJogador efeito) : CartaEfeito(descricao, efeito)
{
}
