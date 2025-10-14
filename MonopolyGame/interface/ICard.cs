using MonopolyPaperMario.Model; // Para o tipo Player

namespace MonopolyPaperMario.Interface
{
    // A interface que todas as cartas devem implementar
    public interface ICard
    {
        // Método que define o efeito da carta no momento em que é tirada.
        void QuandoPegada(Player jogador);
    }
}