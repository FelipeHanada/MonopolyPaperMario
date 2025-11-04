using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame.Impl.Efeitos
{
    internal class EfeitoReverterComprarJogador(Partida partida) : EfeitoJogador(partida)
    {
        public override void Aplicar(Jogador jogador)
        {
            if (jogador == null) throw new ArgumentNullException(nameof(jogador));
            jogador.PodeComprar = !jogador.PodeComprar;
        }
    }
}
