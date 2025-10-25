using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl.Efeitos; // Importação necessária para EfeitoMagikoopaAmarelo
using System;

namespace MonopolyGame.impl.Cartas
{
    internal class CartaMagikoopaVermelho : CartaSorte
    {
        // Define o efeito Magikoopa Amarelo
        public CartaMagikoopaVermelho() 
            : base("Red Magikoopa te deu um boost e durante o próximo turno a quantidade de casas a mover será duplicada.", 
                  new EfeitoMagikoopaVermelho()) // Usando a classe de efeito correta
        {
            
        }
        
        public override void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"Sorte: {Descricao}");
            
            Console.WriteLine("================DEBUG=================\nAgendando a mágica Magikoopa Vermelho.");
            Efeito?.Execute(jogador); // LINHA MAIS IMPORTANTE DA CARTA
            // Agenda o EfeitoMagikoopaAmarelo para ser executado no início do próximo turno do jogador (contador = 1).
            // O efeito é criado aqui para que a Partida possa agendá-lo.
            Partida.GetPartida().addEfeitoTurnoParaJogadores(1, new EfeitoMagikoopaVermelho(), [jogador]);
        }
    }
}