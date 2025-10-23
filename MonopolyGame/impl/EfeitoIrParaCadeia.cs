using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl
{
    public class EfeitoIrParaCadeia : IEfeitoJogador
    {
        public Tabuleiro? Tabuleiro { get; set; }

        public EfeitoIrParaCadeia() { }

        public void Execute(Jogador jogador)
        {
            if (jogador == null) throw new ArgumentNullException(nameof(jogador));
            if (Tabuleiro == null) throw new InvalidOperationException("A referência ao tabuleiro não foi definida para EfeitoIrParaCadeia.");

            if (jogador.CartasPasseLivre > 0)
            {
                // Aqui, você precisaria de uma lógica de input real (interface, console, etc.)
                // para perguntar ao jogador se ele quer usar a carta.
                // Como não temos a UI, vamos assumir que ele prefere USAR a carta
                // para evitar a prisão, que é a situação mais comum de uso imediato.
                bool usarCarta = true; 
                
                if (usarCarta)
                {
                    jogador.CartasPasseLivre--; // Diminui o contador
                    
                    Console.WriteLine($"{jogador.Nome} usou uma Carta Star Beam (Passe Livre) para EVITAR a cadeia!");
                    Console.WriteLine($"Cartas Passe Livre restantes: {jogador.CartasPasseLivre}");
                    
                    // IMPORTANTE: Se esta carta for devolvida ao baralho, 
                    // a lógica de devolução do objeto 'CartaStarBeam' deve ser adicionada aqui.
                    
                    return; // Encerra a execução e o jogador não vai para a cadeia
                }
            }


            Console.WriteLine($"{jogador.Nome} foi para a cadeia!");
            jogador.SetPreso(true);
            
            // Revertido para usar a posição fixa 10
            Tabuleiro.MoverJogadorPara(jogador, 10, false);
        }
    }
}