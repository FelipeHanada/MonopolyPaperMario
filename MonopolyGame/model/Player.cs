class Player
{
    private bool falido;

    private bool preso;

    private int dinheiro;

    private bool cego;

    public Player(int dinheiroInicial)
    {
        dinheiro = dinheiroInicial;
        falido = false;
        preso = false;
        cego = false;
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
}