using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;


class EfeitoDarPasseLivre(int quantidade) : IEfeitoJogador
{
    private readonly int quantidade = quantidade;
    
    public void Aplicar(Jogador jogador)
    {
        jogador.CartasPasseLivre++;
    }
}
