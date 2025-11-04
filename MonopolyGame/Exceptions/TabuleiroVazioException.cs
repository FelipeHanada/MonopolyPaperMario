using System;


namespace MonopolyGame.Exceptions
{
    class TabuleiroVazioException : Exception
    {
        public TabuleiroVazioException() : base("O tabuleiro está vazio. Chame getNovoTabuleiro antes de prosseguir."){}
    }
}