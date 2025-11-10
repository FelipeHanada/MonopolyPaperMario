using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Model.Tabuleiros
{
    public class PosicaoJogador
    {
        public int PosicaoAtual { get; set; }
        public Jogador Jogador { get; private set; }

        public PosicaoJogador(Jogador jogador, int posicaoInicial = 0)
        {
            Jogador = jogador;
            PosicaoAtual = posicaoInicial;
        }
    }
}