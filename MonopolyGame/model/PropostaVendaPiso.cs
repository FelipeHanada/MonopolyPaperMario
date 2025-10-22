namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class PropostaVendaPiso
    {
        public Jogador Vendedor { get; private set; }
        public Jogador Comprador { get; private set; }
        public Propriedade Propriedade { get; private set; }
        public int Valor { get; private set; }

        public PropostaVendaPiso(Jogador vendedor, Jogador comprador, Propriedade propriedade, int valor)
        {
            Vendedor = vendedor;
            Comprador = comprador;
            Propriedade = propriedade;
            Valor = valor;
        }
    }
}