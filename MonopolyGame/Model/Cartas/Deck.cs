using MonopolyGame.Interface.Cartas;

namespace MonopolyGame.Model.Cartas;


public class Deck<T> : IDeck where T : class, ICarta
{
    private List<T> cartas;
    private Random rng = new Random();

    public Deck(List<T> cartasIniciais)
    {
        cartas = cartasIniciais ?? new List<T>();
        Embaralhar();
    }

    public void Embaralhar()
    {
        cartas = cartas.OrderBy(c => rng.Next()).ToList();
    }

    // Implementação do método da interface
    ICarta? IDeck.ComprarCarta()
    {
        return ComprarCartaGenerico();
    }

    public T? ComprarCartaGenerico()
    {
        if (cartas.Count == 0)
        {
            return null;
        }

        T carta = cartas[0];
        cartas.RemoveAt(0);
        return carta;
    }
}
