class Player
{
    private bool falido;

    private bool preso;

    private int dinheiro;

    private bool cego;

    private PlayerPosition posicao;

    public Player(int dinheiroInicial,PlayerPosition posicao)
    {
        dinheiro = dinheiroInicial;
        falido = false;
        preso = false;
        cego = false;
        posicao = this.posicao;
    }

    public void setFalido(bool falido)
    {
        this.falido=falido;
    }

    public void setPreso(bool preso)
    {
        this.preso=preso;
    }

    public void setDinheiro(int dinheiro)
    {
        this.dinheiro=dinheiro;
    }

    public void setCego(bool cego)
    {
        this.cego = cego;
    }

    public void setPosicao(Pla)
}