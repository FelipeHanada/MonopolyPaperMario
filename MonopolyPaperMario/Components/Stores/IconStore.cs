namespace MonopolyPaperMario.Components.Stores;

public enum IconId
{
    Gem,
    Gift,
    Question,
    Train,
    House1,
    House2,
    House3,
    House4,
    House5,
    Hotel,
    Dice0,
    Dice1,
    Dice2,
    Dice3,
    Dice4,
    Dice5,
    Dice6,
}

internal class IconStore
{
    private static IconStore? instance;

    private readonly Dictionary<IconId, string> PathByIconId = new()
    {
        { IconId.Gem, "assets/icon/gem-solid-full.svg" },
        { IconId.Gift, "assets/icon/gift-solid-full.svg" },
        { IconId.Question, "assets/icon/question-solid-full.svg" },
        { IconId.Train, "assets/icon/train-solid-full.svg" },
        { IconId.House1, "assets/housing_markers/1.png" },
        { IconId.House2, "assets/housing_markers/2.png" },
        { IconId.House3, "assets/housing_markers/3.png" },
        { IconId.House4, "assets/housing_markers/4.png" },
        { IconId.House5, "assets/housing_markers/5.png" },
        { IconId.Hotel, "assets/housing_markers/5.png" },
        { IconId.Dice1, "assets/icon/dices/dice-one-solid-full.svg" },
        { IconId.Dice2, "assets/icon/dices/dice-two-solid-full.svg" },
        { IconId.Dice3, "assets/icon/dices/dice-three-solid-full.svg" },
        { IconId.Dice4, "assets/icon/dices/dice-four-solid-full.svg" },
        { IconId.Dice5, "assets/icon/dices/dice-five-solid-full.svg" },
        { IconId.Dice6, "assets/icon/dices/dice-six-solid-full.svg" },
    };

    private IconStore()
    {
    }

    public static IconStore GetInstance()
    {
        if (instance is not null) return instance;
        instance = new IconStore();
        return instance;
    }

    public string GetIconPath(IconId id)
    {
        return PathByIconId[id];
    }

    public string GetIconPath(int id)
    {
        return GetIconPath((IconId)id);
    }

}
