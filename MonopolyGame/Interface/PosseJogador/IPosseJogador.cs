using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Interface.PosseJogador
{
    public interface IPosseJogador
    {
        string Nome { get; }
        Jogador? Proprietario { get; internal set; }
    }
}
