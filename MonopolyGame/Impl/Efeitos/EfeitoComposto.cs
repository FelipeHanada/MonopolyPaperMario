using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;

namespace MonopolyGame.Impl.Efeitos;


class EfeitoComposto(List<IEfeitoJogador> efeitos) : IEfeitoJogador
{
    private readonly List<IEfeitoJogador> efeitos = efeitos;
    
    public void AddEfeito(IEfeitoJogador efeito)
    {
        efeitos.Add(efeito);
    }
    
    public void Aplicar(Jogador jogador)
    {
        foreach (IEfeitoJogador efeito in efeitos) {
            efeito.Aplicar(jogador);
        }
    }
}
