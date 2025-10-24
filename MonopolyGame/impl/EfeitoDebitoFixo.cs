using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Exceptions;
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl
{

    public class EfeitoDebitoFixo : IEfeitoJogador
    {
        private readonly int valor;
        
        // Removemos o 'motivo' do construtor
        public EfeitoDebitoFixo(int valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do débito deve ser positivo.", nameof(valor));
            }
            this.valor = valor;
        }

        public void Execute(Jogador jogador)
        {
            if (jogador == null) return;
            
            // Usamos a Descrição da Carta para o console.log (menos dependência do efeito)
            Console.WriteLine($"{jogador.Nome} deve pagar ${this.valor}.");
            
            // Aplica o desconto Muskular no valor base
            int valorComDesconto = jogador.AplicarDesconto(this.valor);

            Console.WriteLine($"Valor com desconto Muskular (se aplicável): ${valorComDesconto}.");

            try
            {
                // Debita o valor com desconto
                jogador.Debitar(valorComDesconto);
                Console.WriteLine($"Transação concluída. Novo saldo de {jogador.Nome}: ${jogador.Dinheiro}");
            }
            catch (Exceptions.FundosInsuficientesException)
            {
                // Lógica de falência/hipoteca
                Console.WriteLine($"{jogador.Nome} não conseguiu pagar a multa de ${valorComDesconto} e deve tomar medidas (hipotecar ou falir).");
                jogador.SetFalido(true);
            }
        }
    }
}