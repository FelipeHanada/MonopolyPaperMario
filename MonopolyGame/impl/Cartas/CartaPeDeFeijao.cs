using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyPaperMario.MonopolyGame.Impl.Cartas
{
    internal class CartaPeDeFeijao : CartaSorte
    {
        public CartaPeDeFeijao() : base("Você plantou o pé de feijão que te levou até as nuvens. Ao sair, você retornou para Toad Town (pare na partida e ganhe 200)", new EfeitoMoverJogadorPara(Tabuleiro.getTabuleiro(),0))
        {
           
        }
    }
}