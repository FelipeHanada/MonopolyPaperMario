using System.Reflection.Metadata;

class PlayerPosition
{

    private int position;

    private BlobReader tabuleiro;

    private Player jogador;

    public PlayerPosition(int position, BlobReader tabuleiro, Player jogador)
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