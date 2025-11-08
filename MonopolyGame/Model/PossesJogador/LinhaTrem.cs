using MonopolyGame.Model.Partidas;
using System.Linq;

namespace MonopolyGame.Model.PossesJogador
{
    public class LinhaTrem : Propriedade
    {
        private static readonly int[] aluguelPorQuantidade = { 25, 50, 100, 200 };

        public LinhaTrem(string nome) : base(nome, 200, PropriedadeCor.Trem) { }

        public override int CalcularPagamento(Jogador jogador)
        {
            if (Proprietario == null || Hipotecada) return 0;

            int quantidade = Proprietario.Posses.OfType<LinhaTrem>().Count();
            if (quantidade > 0 && quantidade <= aluguelPorQuantidade.Length)
            {
                return aluguelPorQuantidade[quantidade - 1];
            }
            return 0;
        }
    }
}