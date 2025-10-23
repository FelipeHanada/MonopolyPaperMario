using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyPaperMario.MonopolyGame.Impl.Cartas
{
    internal class CartaLavaVulcao : CartaSorte
    {
        public CartaLavaVulcao(Tabuleiro tabuleiro) : base("Você se queimou na lava do vulcão, volte 3 casas.", new EfeitoMoverJogador(tabuleiro,-3))
        {
           
        }
    }
}