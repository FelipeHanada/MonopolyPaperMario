using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl
{
    public class EfeitoComprarCarta : IEfeitoJogador
    {
        private readonly IDeck deck;

        public EfeitoComprarCarta(IDeck deck)
        {
            this.deck = deck ?? throw new ArgumentNullException(nameof(deck));
        }

        public void Execute(Jogador jogador)
        {
            if (jogador == null) throw new ArgumentNullException(nameof(jogador));

            ICarta? carta = deck.ComprarCarta();
            carta?.QuandoPegada(jogador);
        }
    }
}