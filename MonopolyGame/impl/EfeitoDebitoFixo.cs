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
            

            try
            {
                // Debita o valor com desconto
                jogador.Debitar(this.valor);
                Console.WriteLine($"Transação concluída. Novo saldo de {jogador.Nome}: ${jogador.Dinheiro}");
            }
            catch (Exceptions.FundosInsuficientesException)
            {
                // Lógica de falência/hipoteca
                Console.WriteLine($"{jogador.Nome} não conseguiu pagar a multa de ${this.valor} e deve tomar medidas (hipotecar ou falir).");
                jogador.SetFalido(true);
            }
        }
    }
}