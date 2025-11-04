using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Interface
{
    public interface IPosseJogador
    {
        string Nome { get; }
        Jogador? Proprietario { get; internal set; }
    }
}
