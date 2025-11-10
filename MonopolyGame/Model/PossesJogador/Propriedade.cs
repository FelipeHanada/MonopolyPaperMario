using MonopolyGame.Exceptions;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.PosseJogador;

namespace MonopolyGame.Model.PossesJogador;

public enum PropriedadeCor
{
    Marrom = 1,
    Ciano,
    Rosa,
    Laranja,
    Vermelho,
    Amarelo,
    Verde,
    Azul,
    Companhia,
    Trem
}

public abstract class Propriedade : IPosseJogador
{
    public string Nome { get; private set; }
    public PropriedadeCor Cor { get; }
    public int Preco { get; private set; }
    public int ValorHipoteca { get; private set; }
    public bool Hipotecada { get; set; }
    public Jogador? Proprietario { get; set; }

    protected Propriedade(string nome, int preco, int hipoteca, PropriedadeCor cor)
    {
        Nome = nome;
        Preco = preco;
        Cor = cor;
        ValorHipoteca = hipoteca;
        Hipotecada = false;
        Proprietario = null;
    }

    public abstract int CalcularPagamento(Jogador jogador);

    public virtual bool PodeHipotecar()
    {
        if (Proprietario == null || Hipotecada)
        {
            return false;
        }
        return true;
    }

    public void PagarHipoteca()
    {
        if (Proprietario == null || !PodeHipotecar()) return;
        int valor = CalcularPagamento(Proprietario);
        Proprietario!.Debitar(valor);
    }
}
