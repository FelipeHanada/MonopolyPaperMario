using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Model.Cartas;


public class CartaSorte(string descricao, IEfeitoJogador efeito) : CartaEfeito(descricao, efeito)
{
}
