using System;


namespace MonopolyPaperMario.MonopolyGame.Exceptions
{
    class EstadoInvalidoException : Exception
    {
        public EstadoInvalidoException() : base("O estado em questão não é adequado para a ação requerida."){}
    }
}