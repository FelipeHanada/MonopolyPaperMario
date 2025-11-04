using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame.Impl.Efeitos
{
    internal class EfeitoReverterDirecaoJogador(Partida partida) : EfeitoJogador(partida)
    {
        public override void Aplicar(Jogador jogador)
        {
            Console.WriteLine("Revertendo direção do jogador");
            jogador.Reverso = !jogador.Reverso;
            Console.WriteLine(jogador.Reverso);
        }
    }
}
