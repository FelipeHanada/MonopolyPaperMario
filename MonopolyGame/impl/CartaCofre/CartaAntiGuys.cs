using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl; // Para acessar EfeitoDebitoFixo
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl.CartaCofre
{
    internal class CartaAntiGuys : CartaCofre 
    {
        private const int VALOR_DEBITO = 50;

        public CartaAntiGuys() : base(
            // Descrição da Carta
            $"Oh não, você errou 3 questões no castelo do bowser. Ele chamou o esquadrão anti-guy e você gastou todos os itens para derrotá-los. Pague ${VALOR_DEBITO}.", 
            
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