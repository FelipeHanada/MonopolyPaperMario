using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Exceptions;

namespace MonopolyPaperMario.MonopolyGame.Model
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
            this.Nome = nome;
            this.Preco = preco;
            this.valorHipoteca = preco / 2;
            this.Hipotecada = false;
            this.Proprietario = null;
        }

        public abstract int CalcularPagamento(Jogador jogador);

        public void Hipotecar()
        {
            if (this.Proprietario == null)
            {
                throw new JogadoresNaoInformadosException();
            }

            this.Hipotecada = true;
            Proprietario!.Creditar(valorHipoteca);
        }
        
        public void PagarHipoteca()
        {
            if (this.Proprietario == null)
            {
                throw new JogadoresNaoInformadosException();
            }
            
            if (this.Hipotecada == true)
            {
                int valor = CalcularPagamento(this.Proprietario);
                Proprietario!.Debitar(valor);
            }
        }
    }
}