class Player
{
    private bool falido;

    private bool preso;

    private int dinheiro;

    private bool cego;

    private PlayerPosition posicao;

    public Player(int dinheiroInicial, PlayerPosition posicao)
    {
        dinheiro = dinheiroInicial;
        falido = false;
        preso = false;
        cego = false;
        posicao = this.posicao;
    }

    public bool getFalido(bool falido)
    {
        return falido;
    }

    public void setFalido(bool falido)
    {
        this.falido = falido;
    }
    
    public bool getPreso(bool preso)
    {
        return preso;
    }

    public void setPreso(bool preso)
    {
        this.preso = preso;
    }
    
    public int getDinheiro(int dinheiro)
    {
        return dinheiro;
    }

    public void setDinheiro(int dinheiro)
    {
        this.dinheiro=dinheiro;
    }

    public bool getCego(bool cego)
    {
        return cego;
    }

    public void setCego(bool cego)
    {
        this.cego = cego;
    }

    public PlayerPosition getPosicao(PlayerPosition posicao)
    {
        return posicao;
    }

    public void setPosicao(PlayerPosition posicao)
    {
        this.posicao = posicao;
    }
}