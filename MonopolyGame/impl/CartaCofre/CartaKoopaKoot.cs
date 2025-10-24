using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl; // Para acessar EfeitoCreditoFixo
using System;


namespace MonopolyPaperMario.MonopolyGame.Impl.CartaCofre
{
    
    internal class CartaKoopaKoot : MonopolyPaperMario.MonopolyGame.Model.CartaCofre 
    {
        private const int CREDITO = 100;
        
        public CartaKoopaKoot() : base(
            // Descrição da Carta
            $"Você ajudou o koopa koot e ganhou acesso ao playground exclusivo! Recebe ${CREDITO}.", 
            
           
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