using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyPaperMario.MonopolyGame.Impl.Cartas
{
    internal class CartaLuteComBowser : CartaSorte
    {
        public CartaLuteComBowser() : base("Oh não, o Bowser está aqui, lute contra ele e ajude o reino dos cogumelos.", new EfeitoIrParaCadeia(Tabuleiro.getTabuleiro()))
        {
           
        }
    }
}