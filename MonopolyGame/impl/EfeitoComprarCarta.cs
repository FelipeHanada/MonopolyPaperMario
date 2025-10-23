using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl.Cartas; // Importado para reconhecer CartaStarBeam
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

            if (carta == null)
            {
                Console.WriteLine("O baralho está vazio!");
                return;
            }

            // ====================================================================
            // LÓGICA DE TRATAMENTO DA CARTA PASSE LIVRE (STAR BEAM)
            // ====================================================================
            if (carta is CartaStarBeam starBeam)
            {
                // Se for a Carta Star Beam, o jogador a recebe e a possui
                jogador.CartasPasseLivre++;
                Console.WriteLine($"{jogador.Nome} sacou a Carta Star Beam (Passe Livre)! Você agora tem {jogador.CartasPasseLivre} carta(s).");
                
                // NOTA: Não chamamos 'QuandoPegada' aqui porque o efeito é apenas 
                // a posse, e a carta não deve ser devolvida ao baralho neste momento.
            }
            else
            {
                // Para todas as outras cartas (monetárias ou de movimento imediato), 
                // executamos o efeito padrão.
                Console.WriteLine($"{jogador.Nome} sacou uma carta de Ação.");
                carta.QuandoPegada(jogador);
            }
        }
    }
}
