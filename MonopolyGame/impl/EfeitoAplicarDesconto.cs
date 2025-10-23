using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame.impl
{
    internal class EfeitoAplicarDesconto : IEfeitoJogador
    {
        private int percentual;
        public EfeitoAplicarDesconto(int percentual)
        {
            this.percentual = percentual;
        }
        public void Execute(Jogador jogador)
        {
            jogador.Desconto = percentual;
        }
    }
}
