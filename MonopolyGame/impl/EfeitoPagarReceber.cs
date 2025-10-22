using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl
{
    public class EfeitoPagarReceber : IEfeitoJogador
    {
        public int Valor { get; private set; }

        public EfeitoPagarReceber(int valor)
        {
            this.Valor = valor;
        }

        public void Execute(Jogador jogador)
        {
            if (jogador == null) throw new ArgumentNullException(nameof(jogador));

            if (Valor > 0)
            {
                jogador.Creditar(Valor);
                Console.WriteLine($"{jogador.Nome} recebeu ${Valor}.");
            }
            else if (Valor < 0)
            {
                int valorAbsoluto = Math.Abs(Valor);
                try
                {
                    jogador.Debitar(valorAbsoluto);
                    Console.WriteLine($"{jogador.Nome} pagou ${valorAbsoluto}.");
                }
                catch (Exceptions.FundosInsuficientesException ex)
                {
                    Console.WriteLine(ex.Message);
                    jogador.SetFalido(true);
                }
            }
        }
    }
}