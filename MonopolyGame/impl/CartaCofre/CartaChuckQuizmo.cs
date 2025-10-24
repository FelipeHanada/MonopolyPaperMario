using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl; // Para acessar EfeitoCreditoFixo
using System;


namespace MonopolyPaperMario.MonopolyGame.Impl.CartaCofre
{
    internal class CartaChuckQuizmo : MonopolyPaperMario.MonopolyGame.Model.CartaCofre
    {
        private const int CREDITO = 50;
        
        public CartaChuckQuizmo() : base(
            // Descrição da Carta
            $"Chuck Quizmo: Você acertou a questão! Recebe ${CREDITO}.", 
            
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