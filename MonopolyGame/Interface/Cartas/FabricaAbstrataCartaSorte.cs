using MonopolyGame.Model.Cartas;

namespace MonopolyGame.Interface.Cartas;


enum CartasSorte
{
    CartaBowserShuffle,
    CartaBlooper,
    CartaDuplighost,
    CartaGrooveGuyTonto,
    CartaLavaVulcao,
    CartaLuteComBowser,
    CartaMagikoopaAmarelo,
    CartaMagikoopaVermelho,
    CartaMartelo,
    CartaMuskular,
    CartaPeDeFeijao,
    CartaSentinels,
    CartaSpinyTromp,
    CartaStarBeam,
    CartaTimeout,
    CartaTrocaCano,
}


interface FabricaAbstrataCartaSorte
{
    public CartaSorte CriaCarta(CartasSorte cartaId);
    public List<CartaSorte> CriaTodasAsCartas();
}
