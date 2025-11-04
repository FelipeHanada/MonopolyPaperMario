using MonopolyGame.Interface.Cartas;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Model.Cartas;


public abstract class CartaEfeito(string descricao, IEfeitoJogador? efeito) : ICarta
{
    private readonly string descricao = descricao;
    private readonly IEfeitoJogador? efeito = efeito;

    public string GetDescricao()
    {
        return descricao;
    }

    public void QuandoPegada(Jogador jogador)
    {
        efeito?.Aplicar(jogador);
    }
}
