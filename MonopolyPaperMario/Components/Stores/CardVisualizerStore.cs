using MonopolyGame.Model.PossesJogador;

namespace MonopolyPaperMario.Components.Stores;


public enum CardType
{
    TitleDeed,
    Company,
    TrainStation,
}


public class CardVisualizerStore
{
    public event Action? OnStateChanged;

    private static CardVisualizerStore? instance;

    public static CardVisualizerStore Instance => instance ??= new CardVisualizerStore();

    public CardType? CurrentCardType { get; private set; }

    public Imovel? Imovel { get; private set; }
    public Companhia? Companhia { get; private set; }
    public LinhaTrem? LinhaTrem { get; private set; }

    private CardVisualizerStore()
    {
        ControlePartidaStore.GetInstance().OnStateChanged += NotifyStateChanged;
    }

    public void SetImovel(Imovel imovel)
    {
        this.Imovel = imovel;
        CurrentCardType = CardType.TitleDeed;
        NotifyStateChanged();
    }

    public void SetCompanhia(Companhia companhia)
    {
        this.Companhia = companhia;
        CurrentCardType = CardType.Company;
        NotifyStateChanged();
    }

    public void SetLinhaTrem(LinhaTrem linhaTrem)
    {
        this.LinhaTrem = linhaTrem;
        CurrentCardType = CardType.TrainStation;
        NotifyStateChanged();
    }
    
    public void NotifyStateChanged()
    {
        OnStateChanged?.Invoke();
    }
}
