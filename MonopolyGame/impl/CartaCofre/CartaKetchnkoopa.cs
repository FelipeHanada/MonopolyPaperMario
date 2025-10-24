using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl; // Para acessar EfeitoDebitoFixo
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl.CartaCofre
{
    internal class CartaKetchnkoopa: CartaCofre 
    {
        private const int VALOR_DEBITO = 100;

        public CartaKetchnkoopa() : base(
            // Descrição da Carta
            $"Ketch'n Koopa está bloqueando a sua passagem e você o subornou. Pague ${VALOR_DEBITO}.", 
            
            // O Efeito agora só precisa do valor
            new EfeitoDebitoFixo(VALOR_DEBITO)
        )
        {
            
        }
        
        public override void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"Cofre: {Descricao}");
            
            Efeito?.Execute(jogador); 
        }
    }
}