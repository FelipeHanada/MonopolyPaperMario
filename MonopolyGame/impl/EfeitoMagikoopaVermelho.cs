using System;
using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;

namespace MonopolyPaperMario.MonopolyGame.Impl.Efeitos
{

    /// Efeito imediato ativado pelo Magikoopa Vermelho.
    /// Define um multiplicador de 1.5 para a próxima jogada de dados do jogador.

    public class EfeitoMagikoopaVermelho : IEfeitoJogador
    {
       

        public void Execute(Jogador jogadorAlvo)
        {
            Console.WriteLine($"--- Efeito Magikoopa Vermelho ativado para {jogadorAlvo.Nome} ---");
            if (jogadorAlvo.Multiplicador == 0)
            {
                jogadorAlvo.Multiplicador = 2;
            }
            else
            {
                jogadorAlvo.Multiplicador = 0;
            }
            Console.WriteLine("=================DEBUG===============\nMultiplicador: "+jogadorAlvo.Multiplicador);
        }
    }
}