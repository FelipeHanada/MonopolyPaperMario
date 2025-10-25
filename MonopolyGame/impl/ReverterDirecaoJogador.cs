using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame.impl
{
    internal class ReverterDirecaoJogador : IEfeitoJogador
    {
        public void Execute(Jogador jogador)
        {
            Console.WriteLine("Revertendo direção do jogador");
            jogador.Reverso = !jogador.Reverso;
            Console.WriteLine(jogador.Reverso);
        }
    }
}
