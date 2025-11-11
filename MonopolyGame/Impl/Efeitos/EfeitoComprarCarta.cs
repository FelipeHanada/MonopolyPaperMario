using MonopolyGame.Interface.Cartas;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Utils;

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
            Log.WriteLine("O baralho está vazio!");
            return;
        }


        Log.WriteLine("O jogador " + jogador.Nome + " comprou uma carta!");
        Log.WriteLine("Carta sacada: " + carta.GetDescricao());

        jogador.Partida.AdicionarRegistro("O jogador " + jogador.Nome + " comprou uma carta!");
        jogador.Partida.AdicionarRegistro("Carta sacada: " + carta.GetDescricao());

        carta.QuandoPegada(jogador);
    }
}
