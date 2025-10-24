using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MonopolyPaperMario.MonopolyGame.Impl
{
    public class EfeitoDuplighost : IEfeitoJogador
    {
        private readonly Partida partida;

        public EfeitoDuplighost(Partida partida)
        {
            this.partida = partida ?? throw new ArgumentNullException(nameof(partida));
        }

        public void Execute(Jogador jogadorAcionador)
        {
            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine($"!!! {jogadorAcionador.Nome} sacou DUPLIGHOST !!!");

            // 1. Seleciona o 'Duplighost' (o jogador que pagará a próxima conta)
            
            // Filtra jogadores ativos, EXCLUINDO o jogador que sacou a carta (para ser mais interessante)
            var jogadoresAtivos = partida.Jogadores.Where(j => !j.Falido && j != jogadorAcionador).ToList();
            
            if (jogadoresAtivos.Count == 0)
            {
                Console.WriteLine("Não há outros jogadores ativos para o Duplighost se transformar. Efeito cancelado.");
                return;
            }

            // Usa um gerador de números aleatórios para escolher o alvo
            Random random = new Random();
            int indexDuplighost = random.Next(jogadoresAtivos.Count);
            Jogador duplighostAlvo = jogadoresAtivos[indexDuplighost];

            Console.WriteLine($"Duplighost transformou-se em {duplighostAlvo.Nome}! Ele pagará a sua próxima despesa de propriedade/aluguel.");
            
            // 2. Cria o Efeito Agendado de Reversão
            var efeitoReversor = new EfeitoDuplighostReversor(duplighostAlvo);

            // 3. Agenda a reversão para daqui a 1 turno (dura apenas o próximo turno do jogador)
            // O efeito deve se aplicar APENAS ao jogador que sacou a carta.
            partida.addEfeitoTurnoParaJogadores(
                1, // Duração de 1 turno (será removido no início do turno seguinte)
                efeitoReversor,
                new Jogador[] { jogadorAcionador } // O efeito será executado sobre o jogador que sacou a carta
            );
            
            Console.WriteLine("------------------------------------------------\n");
        }
    }
}