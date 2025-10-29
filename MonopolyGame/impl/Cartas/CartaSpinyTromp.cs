using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyPaperMario.MonopolyGame.Impl.Cartas
{
    internal class CartaSpinyTromp : CartaSorte
    {
        public CartaSpinyTromp() : base("Oh não, um Spiny Tromp. Fuja dele e avance 4 casas", new EfeitoMoverJogador(Tabuleiro.getTabuleiro(),4))
        {
           
        }
    }
}