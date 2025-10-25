using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl.Efeitos; // Importação necessária para EfeitoMagikoopaAmarelo
using System;

namespace MonopolyGame.impl.Cartas
{
    internal class CartaMagikoopaAmarelo : CartaSorte
    {
        // Define o efeito Magikoopa Amarelo
        public CartaMagikoopaAmarelo() 
            : base("Yellow Magikoopa ativou sua mágica e agora todos os jogadores no próximo turno lhe pagarão 100 moedas", 
                  new EfeitoMagikoopaAmarelo()) // Usando a classe de efeito correta
        {
            
        }
        
        public override void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"Sorte: {Descricao}");
            
            // O Efeito Magikoopa Amarelo é uma ação AGENDADA, não é executada imediatamente
            
            Console.WriteLine("================DEBUG=================\nAgendando a mágica Magikoopa Amarelo.");
            
            // Agenda o EfeitoMagikoopaAmarelo para ser executado no início do próximo turno do jogador (contador = 1).
            // O efeito é criado aqui para que a Partida possa agendá-lo.
            Partida.GetPartida().addEfeitoTurnoParaJogadores(1, new EfeitoMagikoopaAmarelo(), [jogador]);
        }
    }
}