class Player
{
    private bool falido;

    private bool preso;

    private double dinheiro;

    private bool cego;

    public Player(double dinheiroInicial)
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

    public void setDinheiro(double dinheiro)
    {
        this.dinheiro=dinheiro;
    }

    public void setCego(bool cego)
    {
        this.cego = cego;
    }
}