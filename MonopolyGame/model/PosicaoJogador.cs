using System.Reflection.Metadata;

class PosicaoJogador
{

    private int position;

    private BlobReader tabuleiro;

    private Player jogador;

    public PosicaoJogador(int position, BlobReader tabuleiro, Player jogador)
    {
        this.position = position;
        this.tabuleiro = tabuleiro;
        this.jogador = jogador;
    }

    public getPosition(int position)
    {
        return position;
    }
    
    public void setPosition(int position){

        this.position = position;
    }


}