using System;


namespace MonopolyPaperMario.MonopolyGame.Exceptions
{
    class JogadoresNaoInformadosException : Exception
    {
        public JogadoresNaoInformadosException() : base("Os jogadores não foram informados adequadamente!"){}
    }
}