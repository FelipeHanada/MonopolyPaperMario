using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl; // Para acessar EfeitoDebitoFixo
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl.CartaCofre
{
    internal class CartaMistake : MonopolyPaperMario.MonopolyGame.Model.CartaCofre 
    {
        private const int VALOR_DEBITO = 80;

        public CartaMistake() : base(
            // Descrição da Carta
            $"Você misturou na cozinha da Tayce.T um ultra shroom com uma stone cap e saiu um mistake. Pague ${VALOR_DEBITO}.", 
            
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