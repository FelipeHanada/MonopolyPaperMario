using MonopolyGame.Exceptions;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;


public class EfeitoPagarReceber(Partida partida, int valor) : EfeitoJogador(partida)
{
    public readonly int valor = valor;

    public override void Aplicar(Jogador jogador)
    {
        if (jogador == null) throw new ArgumentNullException(nameof(jogador));

        if (valor > 0)
        {
            // Crédito: Não se aplica desconto.
            jogador.Creditar(valor);
            Console.WriteLine($"{jogador.Nome} recebeu ${valor}.");
        }
        else if (valor < 0)
        {
            // Débito (Despesa/Imposto)
            int valorDespesaBase = Math.Abs(valor);
            
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
            catch (FundosInsuficientesException ex)
            {
                Console.WriteLine(ex.Message);
                jogador.SetFalido(true);
            }
        }
    }
}
