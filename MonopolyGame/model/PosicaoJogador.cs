using System.Reflection.Metadata;
namespace MonopolyPaperMario.model
{
    class PosicaoJogador
    {

        private int position;

        private Tabuleiro tabuleiro;

        private Jogador jogador;

        public PosicaoJogador(int position, Tabuleiro tabuleiro, Player jogador)
        {
            this.position = position;
            this.tabuleiro = tabuleiro;
            this.jogador = jogador;
        }

        public getPosition(int position)
        {
            return position;
        }

        public void setPosition(int position)
        {

            this.position = position;
        }

        public Jogador getJogador()
        {
            return jogador;
        }


    }
}