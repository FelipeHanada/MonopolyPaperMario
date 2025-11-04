using MonopolyGame.Interface;
using MonopolyGame.Interface.PropostasVenda;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PossesJogador;

namespace MonopolyGame.Model.PropostasVenda;

public class PropostaVenda(Jogador? vendedor, Jogador comprador, int deltaDinheiro = 0) : IPropostaVenda
{
    private readonly Jogador? vendedor = vendedor;
    private readonly Jogador comprador = comprador;
    private readonly List<IPosseJogador> possesVendedor = [];
    private readonly List<IPosseJogador> possesComprador = [];
    private int deltaDinheiro = deltaDinheiro;

    public Jogador? GetVendedor()
    {
        return vendedor;
    }
    public Jogador GetComprador()
    {
        return comprador;
    }
    public List<IPosseJogador> GetPossesVendedor()
    {
        return possesVendedor;
    }
    public List<IPosseJogador> GetPossesComprador()
    {
        return possesComprador;
    }
    public int GetDeltaDinheiro()
    {
        return deltaDinheiro;
    }
    public void AddPosseVendedor(IPosseJogador posse)
    {
        possesVendedor.Add(posse);
    }
    public void AddPosseComprador(IPosseJogador posse)
    {
        possesComprador.Add(posse);
    }
    public void AddDinheiroVendedor(int dinheiro)
    {
        deltaDinheiro += dinheiro;
    }
    public void AddDinheiroComprador(int dinheiro)
    {
        deltaDinheiro -= dinheiro;
    }
    public void SetDeltaDinheiro(int novoDeltaDinheiro)
    {
        deltaDinheiro = novoDeltaDinheiro;
    }

    public bool Valido()
    {
        foreach (IPosseJogador posseJogador in possesComprador)
        {
            if (posseJogador.Proprietario != comprador) return false;
        }

        foreach (IPosseJogador posseJogador in possesVendedor)
        {
            if (posseJogador.Proprietario != vendedor) return false;
        }

        if (deltaDinheiro >= 0)
        {
            return vendedor == null || vendedor?.Dinheiro >= deltaDinheiro;
        } else
        {
            return comprador.Dinheiro >= -deltaDinheiro;
        }
    }
}
