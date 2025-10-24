using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl; // Para acessar EfeitoCreditoFixo
using System;


namespace MonopolyPaperMario.MonopolyGame.Impl.CartaCofre
{
    
    internal class CartaWhacka  : MonopolyPaperMario.MonopolyGame.Model.CartaCofre 
    {
        private const int CREDITO = 65;
        
        public CartaWhacka () : base(
            // Descrição da Carta
            $"Você bateu no whacka e recebeu um bump valioso para vendas e o vendeu na Toad's Town Recebe ${CREDITO}.", 
            
            // O Efeito que será executado (Creditar $50)
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