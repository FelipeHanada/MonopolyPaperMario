using MonpolyMario.Components.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonpolyMario.Components.Game.Exceptions
{
    internal class PosseNaoEDoJogadorCorrenteException:Exception
    {
        public PosseNaoEDoJogadorCorrenteException(Jogador jogador, PosseJogador posse) : base("A posse " + posse.getNome() +" não pertence ao jogador "+jogador.getNome())
        {

        }
    }
}
