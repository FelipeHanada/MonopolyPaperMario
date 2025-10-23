using MonopolyPaperMario.MonopolyGame.Model;

namespace MonopolyGame.impl.Cartas
{
    internal class CartaGrooveGuyTonto : CartaSorte
    {
        public CartaGrooveGuyTonto() : base("Groove Guy te deixou tonto. Você vai se mover na direção contrária no próximo turno.", new ReverterDirecaoJogador())
        {
            
        }
        public override void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"Sorte: {Descricao}");
            Efeito?.Execute(jogador);
        }
    }
}
