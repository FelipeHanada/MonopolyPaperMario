namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class PosicaoJogador
    {
        public int PosicaoAtual { get; set; }
        public Tabuleiro Tabuleiro { get; private set; }
        public Jogador Jogador { get; private set; }

        public PosicaoJogador(Jogador jogador, Tabuleiro tabuleiro, int posicaoInicial = 0)
        {
            this.Jogador = jogador;
            this.Tabuleiro = tabuleiro;
            this.PosicaoAtual = posicaoInicial;
        }
    }
}