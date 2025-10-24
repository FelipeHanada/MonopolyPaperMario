using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl; // Para acessar EfeitoDebitoFixo
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl.CartaCofre
{
    internal class CartaChetRippo : MonopolyPaperMario.MonopolyGame.Model.CartaCofre 
    {
        private const int VALOR_DEBITO = 65;

        public CartaChetRippo() : base(
            // Descrição da Carta
            $"Seus stats estão desbalanceados e você pediu para chet rippo balancea-los. Pague ${VALOR_DEBITO}.", 
            
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