using System;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Exceptions
{
    public class FundosInsuficientesException : Exception
    {
        public Jogador Jogador { get; }

        public FundosInsuficientesException(Jogador jogador, string message = "Fundos insuficientes.")
            : base(message)
        {
            this.Jogador = jogador;
        }
    }
}