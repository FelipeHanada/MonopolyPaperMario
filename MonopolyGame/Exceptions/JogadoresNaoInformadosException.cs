using System;


namespace MonpolyMario.Components.Game.Exceptions
{
    class JogadoresNaoInformadosException : Exception
    {
        public JogadoresNaoInformadosException() : base("Os jogadores não foram informados adequadamente!"){}
    }
}