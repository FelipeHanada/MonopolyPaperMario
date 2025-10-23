using MonopolyPaperMario.MonopolyGame.Interface; 
using MonopolyPaperMario.MonopolyGame.Model;    
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl
{
    // Responsável por mover o jogador um número (positivo ou negativo) de casas.
    public class EfeitoMoverJogador : IEfeitoJogador
    {
        // CAMPOS NECESSÁRIOS:
        private int _offset;
        private Tabuleiro _tabuleiro; 

        // CONSTRUTOR: Deve receber o Tabuleiro e o valor do movimento.
        public EfeitoMoverJogador(Tabuleiro tabuleiro, int offset)
        {
            this._tabuleiro = tabuleiro ?? throw new ArgumentNullException(nameof(tabuleiro));
            this._offset = offset;
        }

        // EXECUÇÃO: Usa o Tabuleiro para aplicar o movimento.
        public void Execute(Jogador jogador)
        {
            if (jogador == null)
            {
                throw new ArgumentNullException(nameof(jogador));
            }

            Console.WriteLine($"Efeito: Movendo {jogador.Nome} por {_offset} casas.");

            // O método moveJogador no Tabuleiro lida com a lógica de:
            // 1. Movimento para frente (offset positivo).
            // 2. Movimento para trás (offset negativo).
            // 3. Verificação de ter passado pelo início (apenas se movendo para frente).
            this._tabuleiro.MoveJogador(jogador, this._offset);
        }
    }
}