using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame.impl
{
    internal class ReverterPodeJogar : IEfeitoJogador
    {
        public void Execute(Jogador jogador)
        {
            jogador.PodeJogar = !jogador.PodeJogar;
        }
    }
}
