using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Model.PossesJogador;


public static class Monopolio
{
    // Total de propriedades por cor (exemplo, ajuste conforme seu tabuleiro)
    private static readonly Dictionary<PropriedadeCor, int> TotalPropriedades = new()
    {
        { PropriedadeCor.Marrom, 2 },
        { PropriedadeCor.Ciano, 3 },
        { PropriedadeCor.Rosa, 3 },
        { PropriedadeCor.Laranja, 3 },
        { PropriedadeCor.Vermelho, 3 },
        { PropriedadeCor.Amarelo, 3 },
        { PropriedadeCor.Verde, 3 },
        { PropriedadeCor.Azul, 2 },
        { PropriedadeCor.Companhia, 2 },
        { PropriedadeCor.Trem, 4 },
    };

    public static bool VerificarMonopolio(Jogador jogador, PropriedadeCor cor)
    {
        int totalPropriedadesDaCor = TotalPropriedades[cor];
        if (totalPropriedadesDaCor == 0) return false;

        int propriedadesDoJogador = jogador.Posses
            .OfType<Imovel>()
            .Count(p => p.Cor == cor);

        return propriedadesDoJogador == totalPropriedadesDaCor;
    }
}
