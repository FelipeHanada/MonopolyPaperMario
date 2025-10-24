using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MonopolyPaperMario.MonopolyGame.Impl
{
    public class EfeitoBowserShuffle : IEfeitoJogador
    {
        private readonly Partida partida;

        public EfeitoBowserShuffle(Partida partida)
        {
            this.partida = partida ?? throw new ArgumentNullException(nameof(partida));
        }

        // NOTA: O IEfeitoJogador.Execute(Jogador jogador) recebe apenas um jogador, 
        // mas este efeito afetará TODOS os jogadores da Partida.
        // O jogador passado é tipicamente o jogador que acionou o efeito (ex: tirou a carta).
        public void Execute(Jogador jogadorAcionador)
        {
            // 1. Obter todos os jogadores não falidos
            // O efeito não deve incluir jogadores que já faliram.
            List<Jogador> jogadoresAtivos = partida.Jogadores.Where(j => !j.Falido).ToList();
            
            if (jogadoresAtivos.Count <= 1)
            {
                Console.WriteLine("O Bowser Shuffle não pode ser ativado com apenas um jogador ativo.");
                return;
            }

            // 2. Calcular a soma total do dinheiro
            int dinheiroTotal = jogadoresAtivos.Sum(j => (int)j.Dinheiro);
            int numeroJogadores = jogadoresAtivos.Count;

            // 3. Calcular a média (arredondando para o inteiro mais próximo, como é comum em Monopoly)
            int mediaPorJogador = (int)Math.Round((double)dinheiroTotal / numeroJogadores);

            Console.WriteLine("\n==============================================");
            Console.WriteLine($"!!! BOWSER SHUFFLE ATIVADO por {jogadorAcionador.Nome} !!!");
            Console.WriteLine($"Dinheiro total na partida: ${dinheiroTotal}");
            Console.WriteLine($"Média de dinheiro por jogador: ${mediaPorJogador}");
            Console.WriteLine("==============================================\n");

            // 4. Redistribuir o dinheiro para a média
            foreach (Jogador j in jogadoresAtivos)
            {
                int diferenca = mediaPorJogador - j.Dinheiro;

                if (diferenca > 0)
                {
                    // O jogador deve RECEBER dinheiro (Creditar)
                    j.Creditar(diferenca);
                    Console.WriteLine($"- {j.Nome} recebeu ${diferenca} (Novo Saldo: ${j.Dinheiro}).");
                }
                else if (diferenca < 0)
                {
                    // O jogador deve PAGAR dinheiro (Debitar)
                    int valorAPagar = Math.Abs(diferenca);
                    
                    // Nota: O Bowser Shuffle é geralmente uma taxação/transferência forçada, 
                    // e *não* deve ser afetado pelo desconto Muskular.
                    try
                    {
                        j.Debitar(valorAPagar);
                        Console.WriteLine($"- {j.Nome} pagou ${valorAPagar} (Novo Saldo: ${j.Dinheiro}).");
                    }
                    catch (Exceptions.FundosInsuficientesException)
                    {
                        // Se um jogador não puder pagar o valor (raro, mas possível),
                        // ele paga o que tem e, se ficar devendo, deve hipotecar ou falir.
                        // Para simplificar, vou assumir que a exceção será tratada
                        // externamente ou que o jogador só perde o que tem.
                        Console.WriteLine($"- {j.Nome} tentou pagar ${valorAPagar}, mas faliu no processo.");
                        j.SetFalido(true);
                    }
                }
            }
        }
    }
}