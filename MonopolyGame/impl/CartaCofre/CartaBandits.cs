using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl; // Para acessar EfeitoDebitoFixo
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl.CartaCofre
{
    internal class CartaBandits : CartaCofre 
    {
        private const int VALOR_DEBITO = 50;

        public CartaBandits() : base(
            // Descrição da Carta
            $"Bandits roubaram parte das suas moedas. Pague ${VALOR_DEBITO}.", 
            
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