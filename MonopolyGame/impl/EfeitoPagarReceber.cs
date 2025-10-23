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
                // Crédito: Não se aplica desconto.
                jogador.Creditar(Valor);
                Console.WriteLine($"{jogador.Nome} recebeu ${Valor}.");
            }
            else if (Valor < 0)
            {
                // Débito (Despesa/Imposto)
                int valorDespesaBase = Math.Abs(Valor);
                
                // ==========================================================
                // NOVO: Aplica o desconto do Muskular no valor da despesa
                // O valor final é o que será efetivamente debitado.
                // ==========================================================
                int valorFinal = jogador.AplicarDesconto(valorDespesaBase);

                try
                {
                    jogador.Debitar(valorFinal);
                    // Informa o valor final pago, que já inclui o desconto
                    Console.WriteLine($"{jogador.Nome} pagou ${valorFinal} (Despesa Base: ${valorDespesaBase}).");
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