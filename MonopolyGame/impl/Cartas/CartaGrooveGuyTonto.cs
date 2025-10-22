using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame.impl.Cartas
{
    internal class CartaGrooveGuyTonto : CartaSorte
    {
        public CartaGrooveGuyTonto() : base("Groove Guy te deixou tonto. Você vai se mover na direção contrária no próximo turno.", new ReverterDirecaoJogador())
        {
           
        }
    }
}
