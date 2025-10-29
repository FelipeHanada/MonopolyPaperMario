using System;


namespace MonopolyPaperMario.MonopolyGame.Exceptions
{
    class TabuleiroVazioException : Exception
    {
        public TabuleiroVazioException() : base("O tabuleiro está vazio. Chame getNovoTabuleiro antes de prosseguir."){}
    }
}