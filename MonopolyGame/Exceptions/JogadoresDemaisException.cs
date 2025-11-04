using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MonopolyGame.Exceptions
{
    class JogadoresDemaisException:Exception
    {
        public JogadoresDemaisException():base("Deve haver mais que dois e menos que sete jogadores para iniciar a partida.") {
                    
        }
    }
}
