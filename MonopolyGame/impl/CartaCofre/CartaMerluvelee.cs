using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl; // Para acessar EfeitoCreditoFixo
using System;


namespace MonopolyPaperMario.MonopolyGame.Impl.CartaCofre
{
    
    internal class CartaMerluvelee : MonopolyPaperMario.MonopolyGame.Model.CartaCofre 
    {
        private const int CREDITO = 80;
        
        public CartaMerluvelee() : base(
            // Descrição da Carta
            $"Você consultou merluvelee e encontrou todas as star pieces do jogo parabéns! Recebe ${CREDITO}.", 
            
           
            new EfeitoCreditoFixo(CREDITO)
        )
        {
            // Nota: Se sua classe base for CartaSorte, use-a.
            // Você mencionou 'cofre', então estou usando CartaCofre como base.
        }
        
        public override void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"Cofre: {Descricao}");
            
            // O EfeitoCreditoFixo.Execute(jogador) é chamado aqui.
            Efeito?.Execute(jogador); 
        }
    }
}