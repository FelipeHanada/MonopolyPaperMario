using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;

namespace MonopolyGame.impl
{
    // A Reversão sempre zera o desconto, não importa qual era o valor anterior.
    internal class EfeitoReverterDesconto : IEfeitoJogador
    {
        public void Execute(Jogador jogador)
        {
            if (jogador == null) return;
            Console.WriteLine($"O efeito de desconto acabou. Descontos de {jogador.Nome} resetados.");
            
            // REVERSÃO: Define o desconto de volta para 0%
            jogador.Desconto = 0; 
        }
    }
}