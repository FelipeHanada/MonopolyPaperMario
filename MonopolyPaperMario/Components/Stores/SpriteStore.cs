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

public enum Sprites
{
    Ch_Mario,
    Ch_Kooper,
    Ch_Bombette,
    Ch_Goombario,
    Ch_Lakilester,
    Ch_Parakarry,

    Brd_Start,
    Brd_Jail,
    Brd_ParkingLot,
    Brd_GoToJail,
}


internal class SpriteStore()
{
    private static SpriteStore? instance;

    private readonly Dictionary<Sprites, string> PathBySpriteId = new()
    {
        { Sprites.Ch_Mario, "assets/characters/mario.png" },
        { Sprites.Ch_Kooper, "assets/characters/kooper.png" },
        { Sprites.Ch_Bombette, "assets/characters/bombette.png" },
        { Sprites.Ch_Goombario, "assets/characters/goombario.png" },
        { Sprites.Ch_Lakilester, "assets/characters/lakilester.png" },
        { Sprites.Ch_Parakarry, "assets/characters/parakarry.png" },
        { Sprites.Brd_Start, "assets/board/start.jpg" },
        { Sprites.Brd_Jail, "assets/board/jail_tile.svg" },
        { Sprites.Brd_ParkingLot, "assets/board/parking_lot.jpg" },
        { Sprites.Brd_GoToJail, "assets/board/go_to_jail.png" },
    };

    private readonly Sprites[] SpriteByCharId =
    {
        Sprites.Ch_Mario,
        Sprites.Ch_Kooper,
        Sprites.Ch_Bombette,
        Sprites.Ch_Goombario,
        Sprites.Ch_Lakilester,
        Sprites.Ch_Parakarry,
    };


    public List<CharacterId> CharacterIdByPlayer { get; } = new();

    public static SpriteStore GetInstance()
    {
        if (instance is not null) return instance;
        instance = new SpriteStore();
        return instance;
    }

    public string GetSpritePath(Sprites spriteId)
    {
        return PathBySpriteId[spriteId];
    }

    public string GetCharacterSpritePath(int id)
    {
        return GetSpritePath(SpriteByCharId[id]);
    }

    public string GetCharacterSpritePath(CharacterId id)
    {
        return GetCharacterSpritePath((int)id);
    }

    public string GetSpritePathByPlayerId(int playerId)
    {
        return GetCharacterSpritePath(CharacterIdByPlayer[playerId]);
    }
}
