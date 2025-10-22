using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl
{
    public class EfeitoSairDaCadeia : IEfeitoJogador
    {
        public void Execute(Jogador jogador)
        {
            if (jogador == null) throw new ArgumentNullException(nameof(jogador));

            if (jogador.Preso)
            {
                jogador.SetPreso(false);
                Console.WriteLine($"{jogador.Nome} está livre da prisão!");
            }
        }
    }
}