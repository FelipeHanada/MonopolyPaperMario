using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;

namespace MonopolyGame.Impl.Efeitos;


class EfeitoComposto(Partida partida, List<IEfeitoJogador> efeitos) : EfeitoJogador(partida)
{
    private readonly List<IEfeitoJogador> efeitos = efeitos;
    
    public void AddEfeito(IEfeitoJogador efeito)
    {
        efeitos.Add(efeito);
    }
    
    public override void Aplicar(Jogador jogador)
    {
        foreach (IEfeitoJogador efeito in efeitos) {
            efeito.Aplicar(jogador);
        }
    }
}
