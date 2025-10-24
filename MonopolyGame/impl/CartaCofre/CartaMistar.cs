using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl; // Para acessar EfeitoCreditoFixo
using System;


namespace MonopolyPaperMario.MonopolyGame.Impl.CartaCofre
{
    
    internal class CartaMistar  : MonopolyPaperMario.MonopolyGame.Model.CartaCofre 
    {
        private const int CREDITO = 200;
        
        public CartaMistar () : base(
            // Descrição da Carta
            $"Você salvou a mistar e com a ajuda dela você conseguiu vencer os inimigos Recebe ${CREDITO}.", 
            
            
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