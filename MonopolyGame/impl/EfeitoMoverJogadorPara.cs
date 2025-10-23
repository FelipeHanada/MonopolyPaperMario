using MonopolyPaperMario.MonopolyGame.Interface; 
using MonopolyPaperMario.MonopolyGame.Model;    
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl
{
    public class EfeitoMoverJogadorPara : IEfeitoJogador
    {
        int posicao;
        private Tabuleiro _tabuleiro; 

        // CONSTRUTOR: Deve receber o Tabuleiro e o valor do movimento.
        public EfeitoMoverJogadorPara(Tabuleiro tabuleiro, int posicao)
        {
            this._tabuleiro = tabuleiro ?? throw new ArgumentNullException(nameof(tabuleiro));
            this.posicao = posicao;
        }

        // EXECUÇÃO: Usa o Tabuleiro para aplicar o movimento.
        public void Execute(Jogador jogador)
        {
            if (jogador == null)
            {
                throw new ArgumentNullException(nameof(jogador));
            }

            Console.WriteLine($"Efeito: Movendo {jogador.Nome} para casa 0.");

            // O método moveJogador no Tabuleiro lida com a lógica de:
            // 1. Movimento para frente (offset positivo).
            // 2. Movimento para trás (offset negativo).
            // 3. Verificação de ter passado pelo início (apenas se movendo para frente).
            this._tabuleiro.MoverJogadorPara(jogador, posicao, true);
        }
    }
}