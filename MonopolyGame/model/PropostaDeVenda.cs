using MonopolyPaperMario.MonopolyGame.Interface;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class PropostaDeVenda
    {
        public Jogador Vendedor { get; private set; }
        public Jogador Comprador { get; private set; }
        public IPosseJogador Posse { get; private set; }
        public int Valor { get; private set; }

        public PropostaDeVenda(Jogador vendedor, Jogador comprador, IPosseJogador posse, int valor) // Alterado para IPosseJogador
        {
            Vendedor = vendedor;
            Comprador = comprador;
            Posse = posse;
            Valor = valor;
        }
    }
}