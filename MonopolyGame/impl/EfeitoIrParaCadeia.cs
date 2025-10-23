using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl
{
    public class EfeitoIrParaCadeia : IEfeitoJogador
    {
        // Nota: A classe Tabuleiro precisa ser acessível para mover o jogador.
        public Tabuleiro? Tabuleiro { get; set; }

        public EfeitoIrParaCadeia() { }

        public void Execute(Jogador jogador)
        {
            if (jogador == null) throw new ArgumentNullException(nameof(jogador));
            if (Tabuleiro == null) throw new InvalidOperationException("A referência ao tabuleiro não foi definida para EfeitoIrParaCadeia.");

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


            // 3. O jogador vai para a prisão (se não usou a carta ou se não tinha nenhuma)
            Console.WriteLine($"\n{jogador.Nome} foi para a cadeia!");
            jogador.SetPreso(true);
            
            // Revertido para usar a posição fixa 10 (a posição da cadeia)
            // O 'false' indica que não deve passar pelo ponto de partida (Go)
            Tabuleiro.MoverJogadorPara(jogador, 10, false);
        }
    }
}
