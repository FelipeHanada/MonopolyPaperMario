using MonopolyPaperMario.MonopolyGame.Model;

namespace MonopolyPaperMario.MonopolyGame.Interface
{
    public interface IPosseJogador
    {
        string Nome { get; }
        Jogador? Proprietario { get; set; }
    }
}