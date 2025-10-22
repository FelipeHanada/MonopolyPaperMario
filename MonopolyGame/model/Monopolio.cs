using System.Linq;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public static class Monopolio
    {
        // Total de propriedades por cor (exemplo, ajuste conforme seu tabuleiro)
        private static readonly int TotalRoxo = 2;
        private static readonly int TotalAzulClaro = 3;
        private static readonly int TotalMarrom = 2;
        private static readonly int TotalVerde = 3;
        private static readonly int TotalAzulEscuro = 3;
        private static readonly int TotalAAmarelo = 3;
        private static readonly int TotalLaranja = 3;
        private static readonly int TotalVermelho = 3;

        public static bool VerificarMonopolio(Jogador jogador, string cor)
        {
            int totalPropriedadesDaCor = 0;
            switch (cor.ToLower())
            {
                case "roxo": totalPropriedadesDaCor = TotalRoxo; break;
                case "azul claro": totalPropriedadesDaCor = TotalAzulClaro; break;
                case "marrom": totalPropriedadesDaCor = TotalMarrom; break;
                case "verde": totalPropriedadesDaCor = TotalVerde; break;
                case "azul escuro": totalPropriedadesDaCor = TotalAzulEscuro; break;
                case "amarelo": totalPropriedadesDaCor = TotalAAmarelo; break;
                case "laranja": totalPropriedadesDaCor = TotalLaranja; break;
                case "vermelho": totalPropriedadesDaCor = TotalVermelho; break;
                default: return false;
            }

            if (totalPropriedadesDaCor == 0) return false;

            int propriedadesDoJogador = jogador.Posses
                .OfType<Imovel>()
                .Count(p => p.Cor.Equals(cor, System.StringComparison.OrdinalIgnoreCase));

            return propriedadesDoJogador == totalPropriedadesDaCor;
        }
    }
}