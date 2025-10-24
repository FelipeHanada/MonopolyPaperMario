using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl
{
    // Efeito genérico para creditar um valor fixo (prêmio).
    public class EfeitoCreditoFixo : IEfeitoJogador
    {
        private readonly int valor;

        public EfeitoCreditoFixo(int valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do crédito deve ser positivo.", nameof(valor));
            }
            this.valor = valor;
        }

        public void Execute(Jogador jogador)
        {
            if (jogador == null) return;
            
            // O jogador recebe o valor.
            // O Creditar() já deve lidar com a atualização do saldo.
            jogador.Creditar(this.valor);
            
            Console.WriteLine($"{jogador.Nome} acertou a questão e recebe ${this.valor}!");
        }
    }
}