using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame.impl.Cartas
{
    internal class CartaBlooper : CartaSorte
    {
        public CartaBlooper() : base("Blooper jogou tinta em você e você não pode ver nada. Fique 1 rodada sem comprar qualquer propriedade ou companhia.", new EfeitoReverterComprarJogador())
        {
           
        }
        public override void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"Sorte: {Descricao}");
            Efeito?.Execute(jogador);
            Console.WriteLine("================DEBUG=================\nAgendando reversão do efeito da carta.");
            Partida.GetPartida().addEfeitoTurnoParaJogadores(1, new EfeitoReverterComprarJogador(), [jogador]);
        }
    }
}
