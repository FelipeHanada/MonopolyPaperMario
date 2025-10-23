using MonopolyPaperMario.MonopolyGame.Interface; 
using MonopolyPaperMario.MonopolyGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyPaperMario.MonopolyGame.Impl
{
    public class EfeitoTrocaPosicaoDinamica : IEfeitoJogador
    {
        private readonly Tabuleiro _tabuleiro; 
        private readonly List<Jogador> _jogadoresAtivos; 
        
        // Adicionamos a lista de jogadores no construtor.
        public EfeitoTrocaPosicaoDinamica(Tabuleiro tabuleiro, List<Jogador> jogadoresAtivos)
        {
            this._tabuleiro = tabuleiro ?? throw new ArgumentNullException(nameof(tabuleiro));
            this._jogadoresAtivos = jogadoresAtivos ?? throw new ArgumentNullException(nameof(jogadoresAtivos));
        }

        public void Execute(Jogador jogadorSolicitante)
        {
            if (jogadorSolicitante == null) throw new ArgumentNullException(nameof(jogadorSolicitante));

            // 1. Encontrar alvos válidos (todos, exceto o solicitante)
            var alvosValidos = _jogadoresAtivos.Where(j => j != jogadorSolicitante).ToList();

            if (!alvosValidos.Any())
            {
                Console.WriteLine($"[AVISO] Efeito Troca Posição: Não há outros jogadores no jogo para trocar.");
                return;
            }

            // um jogador aleatório da lista de alvos válidos.
            Random rand = new Random();
            Jogador jogadorAlvo = alvosValidos[rand.Next(alvosValidos.Count)];
            
            // 3. Execução da Troca
            Console.WriteLine($"Efeito: {jogadorSolicitante.Nome} (você) encontrou um cano! Trocando de posição com {jogadorAlvo.Nome}.");

            this._tabuleiro.TrocarPosicao(jogadorSolicitante, jogadorAlvo);
        }
    }
}