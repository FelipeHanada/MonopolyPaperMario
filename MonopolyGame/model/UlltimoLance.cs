using MonpolyMario.Components.Game.Model;

public class UltimoLance
{
    private int valor;
    private Jogador jogador;

    public UltimoLance(Jogador jogador, int valor)
    {
        this.jogador = jogador;
        this.valor = valor;
    }

    public int getUltimolance()
    {
        return valor;
    }

    public Jogador getJogador()
    {
        return this.jogador;
    }

    public void setUltimoLance(int valor)
    {
        this.valor = valor;
        return;
    }
    
    public void setJogador(Jogador jogador)
    {
        this.jogador = jogador;
        return;
    }
}