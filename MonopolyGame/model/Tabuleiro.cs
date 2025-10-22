using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class Tabuleiro
    {
        private readonly Piso[] pisos;
        private readonly List<PosicaoJogador> posicoesJogadores;

        public Tabuleiro(Piso[] pisos, List<Jogador> jogadores)
        {
            this.pisos = pisos;
            this.posicoesJogadores = new List<PosicaoJogador>();
            foreach (var jogador in jogadores)
            {
                posicoesJogadores.Add(new PosicaoJogador(jogador, this, 0));
            }
        }

        public int GetPosicao(Jogador jogador)
        {
            return posicoesJogadores.FirstOrDefault(p => p.Jogador == jogador)?.PosicaoAtual ?? -1;
        }

        public void MoveJogador(Jogador jogador, int offset)
        {
            PosicaoJogador? posAtual = posicoesJogadores.FirstOrDefault(p => p.Jogador == jogador);
            if (posAtual == null) return;

            int posAnterior = posAtual.PosicaoAtual;
            int novaPosicao = (posAnterior + offset) % this.pisos.Length;

            if (novaPosicao < posAnterior && offset > 0)
            {
                Console.WriteLine($"{jogador.Nome} passou pelo Ponto de Partida e coletou $200!");
                jogador.Creditar(200);
            }

            posAtual.PosicaoAtual = novaPosicao;
            Piso pisoAtual = pisos[novaPosicao];

            Console.WriteLine($"{jogador.Nome} moveu-se para a casa {novaPosicao}: '{pisoAtual.Nome}'.");
            pisoAtual.Efeito(jogador);
        }

        public void MoverJogadorPara(Jogador jogador, int posicao, bool coletarSalario)
        {
            PosicaoJogador? posAtual = posicoesJogadores.FirstOrDefault(p => p.Jogador == jogador);
            if (posAtual == null) return;

            if (coletarSalario && posicao < posAtual.PosicaoAtual)
            {
                Console.WriteLine($"{jogador.Nome} passou pelo Ponto de Partida e coletou $200!");
                jogador.Creditar(200);
            }

            posAtual.PosicaoAtual = posicao;
            Console.WriteLine($"{jogador.Nome} foi movido para a casa {posicao}: '{pisos[posicao].Nome}'.");
        }
    }
}