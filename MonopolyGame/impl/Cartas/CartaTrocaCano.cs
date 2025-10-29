using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl; // Para o EfeitoTrocaPosicaoDinamica
using System.Collections.Generic;

namespace MonopolyGame.impl.Cartas
{
    public class CartaTrocaCano : CartaSorte
    {
        public CartaTrocaCano(List<Jogador> jogadoresAtivos) 
            : base("Você encontrou uma passagem em um cano. Troque de lugar com outro jogador.", 
                   new EfeitoTrocaPosicaoDinamica(Tabuleiro.getTabuleiro(), jogadoresAtivos)) // Injeta as dependências
        {
            
        }
    }
}