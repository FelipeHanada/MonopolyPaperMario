namespace MonopolyPaperMario.Components.Stores;

public enum CharacterId
{
    Mario = 0,
    Kooper = 1,
    Bombette = 2,
    Goombario = 3,
    Lakilester = 4,
    Parakarry = 5
}

internal class CharacterSpriteStore
{
    private static CharacterSpriteStore? instance;

    private readonly string[] PathByCharId =
    {
        "assets/characters/mario.png",
        "assets/characters/kooper.png",
        "assets/characters/bombette.png",
        "assets/characters/goombario.png",
        "assets/characters/lakilester.png",
        "assets/characters/parakarry.png",
    };

    public List<CharacterId> CharacterIdByPlayer { get; } = new();

    private CharacterSpriteStore()
    {
    }

    public static CharacterSpriteStore GetInstance()
    {
        if (instance is not null) return instance;
        instance = new CharacterSpriteStore();
        return instance;
    }

    public string GetSpritePath(int id)
    {
        return PathByCharId[id];
    }

    public string GetSpritePath(CharacterId id)
    {
        return GetSpritePath((int)id);
    }

    public string GetSpritePathByPlayerId(int playerId)
    {
        return GetSpritePath(CharacterIdByPlayer[playerId]);
    }
}
