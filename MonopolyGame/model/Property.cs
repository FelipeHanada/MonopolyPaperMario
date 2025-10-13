class Property
{
    private bool hipotecado;

    private int valorHipoteca;

    public Property(int valorHipoteca)
    {
        this.valorHipoteca = valorHipoteca;
        this.hipotecado = false;
    }

    public bool getHipotecado(bool hipotecado)
    {
        return hipotecado;
    }

    public int getValorHipoteca(int valorHipoteca)
    {
        return valorHipoteca;
    }

    public void setHipotecado(bool hipotecado)
    {
        this.hipotecado = hipotecado;
    }

    public void setValorHipoteca(int valorHipoteca)
    {
        this.valorHipoteca = valorHipoteca;
    }

    public abstract int CalculaPagamento();

}