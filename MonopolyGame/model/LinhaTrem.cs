using System.Linq;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class LinhaTrem : Propriedade
    {
        private static readonly int[] aluguelPorQuantidade = { 25, 50, 100, 200 };

        public LinhaTrem(string nome) : base(nome, 200) { }

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