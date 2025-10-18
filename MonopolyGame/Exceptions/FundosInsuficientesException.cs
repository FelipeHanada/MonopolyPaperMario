using MonpolyMario.Components.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonpolyMario.Components.Game.Exceptions
{
    class FundosInsuficientesException:Exception
    {
        public FundosInsuficientesException(Jogador jogador) : base("O jogador "+jogador.getNome()+" não tem fundos suficientes para realizar essa transferência.") { }
    }
}
