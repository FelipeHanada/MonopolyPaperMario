using MonopolyGame.Interface.Controles;
using MonopolyGame.Impl.Controles;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PossesJogador;
using MonopolyGame.Interface.PosseJogador;

namespace MonopolyPaperMario.Components.Stores;

internal class ControlePartidaStore
{
    private static ControlePartidaStore? instance;
    private ControlePartida? controlePartida;
    public event Action? OnStateChanged;

    public static ControlePartidaStore GetInstance()
    {
        if (instance == null)
        {
            instance = new();
        }
        return instance;
    }

    public void IniciarPartida(Partida partida)
    {
        controlePartida = new ControlePartida(partida);
    }

    public IControlePartida ControlePartida
    {
        get
        {
            if (controlePartida == null) throw new Exception("não tem partida.");
            return controlePartida;
        }
    }

    public void NotifyStateChanged()
    {
        OnStateChanged?.Invoke();
    }
}
