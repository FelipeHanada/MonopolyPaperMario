using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyPaperMario.MonopolyGame.Impl.Cartas
{
    internal class CartaMartelo : CartaSorte
    {
        public CartaMartelo() : base("Você encontrou o martelo e quebrou o bloco que bloqueava a passagem para Toad Town, avance 3 casas", new EfeitoMoverJogador(Tabuleiro.getTabuleiro(),3))
        {
           
        }
    }
}