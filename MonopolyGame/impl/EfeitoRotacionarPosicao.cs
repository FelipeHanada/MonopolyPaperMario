using MonopolyPaperMario.Interface; 
using MonopolyPaperMario.MonopolyGame.Model;
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl
{
    // Efeito que rotaciona as posições de todos os jogadores no tabuleiro.
    public class EfeitoRotacionarPosicao : IEfeitoJogador
    {
        private readonly Tabuleiro _tabuleiro; 
        
        public EfeitoRotacionarPosicao(Tabuleiro tabuleiro) 
        {
            this._tabuleiro = tabuleiro ?? throw new ArgumentNullException(nameof(tabuleiro));
        }

        public void Execute(Jogador jogador)
        {
            // O jogador 'jogador' é o que pegou a carta, mas o efeito é GLOBAL.
            Console.WriteLine($"Efeito Sentinel Ativado! Jogador {jogador.Nome} iniciou a rotação de todas as posições.");
            
            this._tabuleiro.RotacionarPosicoesJogadores();
        }
    }
}