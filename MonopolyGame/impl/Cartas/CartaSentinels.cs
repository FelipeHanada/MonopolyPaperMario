using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl; // Para EfeitoRotacionarPosicao

namespace MonopolyGame.impl.Cartas
{
    public class CartaSentinels : CartaSorte
    {
        public CartaSentinels() 
            : base("Os Sentinels pegaram todos os jogadores e os trocaram de lugar uns com os outros.", 
                   new EfeitoRotacionarPosicao(Tabuleiro.getTabuleiro())) 
        {
            
        }
    }
}