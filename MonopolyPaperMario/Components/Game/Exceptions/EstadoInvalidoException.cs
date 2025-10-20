using System;


namespace MonpolyMario.Components.Game.Exceptions
{
    class EstadoInvalidoException : Exception
    {
        public EstadoInvalidoException() : base("O estado em questão não é adequado para a ação requerida."){}
    }
}