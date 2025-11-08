using MonopolyGame.Model.PossesJogador;

namespace MonopolyPaperMario.Components.Stores;


enum CardType
{
    TitleDeed,
    Company,
    TrainStation,
}


internal class CardVisualizerStore
{
    public event Action? OnStateChanged;

    private static CardVisualizerStore? instance;

    public static CardVisualizerStore Instance => instance ??= new CardVisualizerStore();

    public CardType CurrentCardType { get; private set; } = CardType.TitleDeed;

    public Imovel? Imovel { get; private set; } = new Imovel("Teste", 100, PropriedadeCor.Vermelho, [10, 50, 150, 450, 625, 750], 50, 25);
    public Companhia? Companhia { get; private set; }
    public LinhaTrem? LinhaTrem { get; private set; }

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
