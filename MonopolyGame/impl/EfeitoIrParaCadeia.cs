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

            Console.WriteLine($"{jogador.Nome} foi para a cadeia!");
            jogador.SetPreso(true);
            
            // Revertido para usar a posição fixa 10
            Tabuleiro.MoverJogadorPara(jogador, 10, false);
        }
    }
}