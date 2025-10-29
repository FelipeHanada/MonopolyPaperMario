using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl
{
    public class EfeitoIrParaCadeia : IEfeitoJogador
    {
        // Nota: A classe Tabuleiro precisa ser acessível para mover o jogador.
        private Tabuleiro tabuleiro;

        public EfeitoIrParaCadeia(Tabuleiro tabuleiro)
        {
            this.tabuleiro = tabuleiro;
        }

        public void Execute(Jogador jogador)
        {
            // 1. Verifica se o jogador tem o Passe Livre
            if (jogador.CartasPasseLivre > 0)
            {
                Console.WriteLine($"\n--- AVISO: PRISÃO IMINENTE ---");
                Console.WriteLine($"{jogador.Nome}, você tem {jogador.CartasPasseLivre} carta(s) de Passe Livre (Star Beam).");
                Console.Write("Deseja usar uma para evitar a prisão? (s/n): ");

                // 2. Lógica de Input para a escolha do jogador
                string? resposta = Console.ReadLine()?.Trim().ToLower();

                if (resposta == "s")
                {
                    jogador.CartasPasseLivre--; // Diminui o contador
                    
                    Console.WriteLine($"{jogador.Nome} usou uma Carta Star Beam (Passe Livre) para EVITAR a cadeia!");
                    Console.WriteLine($"Cartas Passe Livre restantes: {jogador.CartasPasseLivre}");
                    
                    // IMPORTANTE: Adicione aqui a lógica para DEVOLVER a carta
                    // ao monte 'Sorte' ou 'Reves', se o seu jogo usa essa regra.
                    
                    return; // Encerra a execução: o jogador não vai para a cadeia
                }
            }

            Console.WriteLine($"\n{jogador.Nome} foi para a cadeia!");
            jogador.SetPreso(true);

            tabuleiro.MoverJogadorPara(jogador, 10, false);
        }
    }
}
