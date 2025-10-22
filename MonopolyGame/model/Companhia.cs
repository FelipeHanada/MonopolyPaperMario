using System.Linq;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class Companhia : Propriedade
    {
        private static readonly int[] multiplicadores = { 4, 10 };

        public Companhia(string nome) : base(nome, 150) { }

        public override int CalcularPagamento(Jogador jogador)
        {
            if (Proprietario == null || Hipotecada) return 0;

            int quantidade = Proprietario.Posses.OfType<Companhia>().Count();
            if (quantidade > 0 && quantidade <= multiplicadores.Length)
            {
                // O cálculo do aluguel depende do valor dos dados.
                // Precisamos de uma forma de obter o último resultado dos dados do turno.
                // Vamos usar um valor fixo para demonstração.
                int ultimoLancamentoDados = 7; // Placeholder
                return multiplicadores[quantidade - 1] * ultimoLancamentoDados;
            }
            return 0;
        }
    }
}