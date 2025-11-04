using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame.Impl.Efeitos
{
    internal class EfeitoDarPasseLivre(Partida partida, int quantidade) : EfeitoJogador(partida)
    {
        private readonly int quantidade = quantidade;
        
        public override void Aplicar(Jogador jogador)
        {
            jogador.CartasPasseLivre++;
        }
    }
}
