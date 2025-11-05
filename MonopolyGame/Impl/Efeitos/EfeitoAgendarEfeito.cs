using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;

namespace MonopolyGame.Impl.Efeitos;


internal class EfeitoAgendarEfeito(IEfeitoJogador efeito, int delta) : IEfeitoJogador
{
    private readonly IEfeitoJogador efeito = efeito;
    private readonly int delta = delta;
    
    public void Aplicar(Jogador jogador)
    {
        jogador.Partida.AddEfeitoTurnoParaJogadores(delta, efeito, [jogador]);
    }
}
