using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Assumindo que o EfeitoBowserShuffle está neste namespace
using MonopolyPaperMario.MonopolyGame.Impl; 

namespace MonopolyPaperMario.MonopolyGame.Impl.Cartas
{
    // NOVO: A classe não precisa mais do Tabuleiro, pois o EfeitoMoverJogador não será usado.
    internal class BowserShuffle : CartaSorte 
    {
        // O construtor não precisa mais de 'Tabuleiro', pois o EfeitoBowserShuffle precisa da 'Partida'.
        public BowserShuffle() : base(
            "Bowser ativou a sua Star Rod e fez com que todos os jogadores em campo tenham o mesmo dinheiro.", 
            // CORREÇÃO: Usar o EfeitoBowserShuffle e injetar a Partida (o gerenciador de estado)
            new EfeitoBowserShuffle(Partida.GetPartida())
        )
        {
            
        }
        
        // A lógica do método QuandoPegada já faz a chamada do Execute do Efeito
        public override void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"Sorte: {Descricao}");
            Efeito?.Execute(jogador); 
        }
    }
}