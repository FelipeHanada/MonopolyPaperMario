using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Utils;

namespace MonopolyGame.Impl.Efeitos;


class EfeitoDarPasseLivre(int quantidade) : IEfeitoJogador
{
    private readonly int quantidade = quantidade;
    
    public void Aplicar(Jogador jogador)
    {
        Log.WriteLine("O jogador " + jogador.Nome + " ganhou um passe livre!");
        jogador.CartasPasseLivre++;
    }
}
