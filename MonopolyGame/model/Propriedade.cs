using MonopolyPaperMario.MonopolyGame.Interface;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public abstract class Propriedade : IPosseJogador
    {
        public string Nome { get; private set; }
        public int Preco { get; private set; }
        public int Hipoteca { get; private set; }
        public bool Hipotecada { get; private set; }
        public Jogador? Proprietario { get; set; }

        protected Propriedade(string nome, int preco)
        {
            this.Nome = nome;
            this.Preco = preco;
            this.Hipoteca = preco / 2;
            this.Hipotecada = false;
            this.Proprietario = null;
        }

        public abstract int CalcularPagamento(Jogador jogador);

        public void SetHipotecada(bool hipotecada)
        {
            this.Hipotecada = hipotecada;
        }
    }
}