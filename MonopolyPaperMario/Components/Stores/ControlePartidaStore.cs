using MonopolyGame.Interface.Controles;
using MonopolyGame.Impl.Controles;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PossesJogador;
using MonopolyGame.Interface.PosseJogador;

namespace MonopolyPaperMario.Components.Stores;

internal class ControlePartidaStore
{
    public static event Action? OnStateChanged;

    private static IControlePartida? instance;

    public static IControlePartida ControlePartida
    {
        get
        {
            if (instance == null)
            {
                instance = new ControlePartida(new Partida(["mario", "papagaio", "princesa"]));
            }
            return instance;
        }
    }

    public static void NotifyStateChanged()
    {
        OnStateChanged?.Invoke();
    }
}
