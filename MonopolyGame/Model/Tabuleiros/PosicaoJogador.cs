using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Model.Tabuleiros
{
    public class PosicaoJogador
    {
        public int PosicaoAtual { get; set; }
        public Tabuleiro Tabuleiro { get; private set; }
        public Jogador Jogador { get; private set; }

        public PosicaoJogador(Jogador jogador, Tabuleiro tabuleiro, int posicaoInicial = 0)
        {
            Jogador = jogador;
            Tabuleiro = tabuleiro;
            PosicaoAtual = posicaoInicial;
        }
    }
}