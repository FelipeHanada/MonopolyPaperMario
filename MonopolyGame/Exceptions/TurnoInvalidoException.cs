using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MonopolyPaperMario.MonopolyGame.Exceptions
{
    class TurnoInvalidoException:Exception
    {
        public TurnoInvalidoException():base("O valor do turno deve estar entre 0 e o número de jogadores da partida.") {
                    
        }
    }
}
