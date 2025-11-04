using MonopolyGame.Interface;
using MonopolyGame.Exceptions;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Model.PossesJogador
{
    public abstract class Propriedade : IPosseJogador
    {
        public string Nome { get; private set; }
        public int Preco { get; private set; }
        public int valorHipoteca { get; private set; }
        public bool Hipotecada { get; private set; }
        public Jogador? Proprietario { get; set; }

        protected Propriedade(string nome, int preco)
        {
            Nome = nome;
            Preco = preco;
            valorHipoteca = preco / 2;
            Hipotecada = false;
            Proprietario = null;
        }

        public abstract int CalcularPagamento(Jogador jogador);

        public void Hipotecar()
        {
            if (Proprietario == null)
            {
                throw new JogadoresNaoInformadosException();
            }

            Hipotecada = true;
            Proprietario!.Creditar(valorHipoteca);
        }
        
        public void PagarHipoteca()
        {
            if (Proprietario == null)
            {
                throw new JogadoresNaoInformadosException();
            }
            
            if (Hipotecada == true)
            {
                int valor = CalcularPagamento(Proprietario);
                Proprietario!.Debitar(valor);
            }
        }
    }
}