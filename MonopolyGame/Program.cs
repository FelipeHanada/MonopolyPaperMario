using System;
using System.Linq;
using MonopolyPaperMario.MonopolyGame.Model;

namespace MonopolyPaperMario.MonopolyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🎲 Monopoly Paper Mario! 🎲");

            var partida = Partida.GetPartida();
            partida.AdicionarJogador("Mario");
            partida.AdicionarJogador("Luigi");
            partida.AdicionarJogador("Peach");
            partida.AdicionarJogador("Yoshi");
            
            partida.IniciarPartida();

            // Loop principal do jogo
            while (partida.Jogadores.Count(j => !j.Falido) > 1)
            {
                partida.ProximoTurno(); // Apenas avança o ponteiro do jogador atual

                // A lógica de "pressione enter" foi movida para dentro de TurnoJogador
                TurnoJogador.Instance.IniciarTurno(partida);
            }

            var vencedor = partida.Jogadores.FirstOrDefault(j => !j.Falido);
            if (vencedor != null)
            {
                Console.WriteLine($"\n🎉 Fim de jogo! O vencedor é {vencedor.Nome}! 🎉");
            }
        }
    }
}