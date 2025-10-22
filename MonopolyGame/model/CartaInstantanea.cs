using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public abstract class CartaInstantanea : ICarta
    {
        public string Descricao { get; protected set; }

        protected CartaInstantanea(string descricao) // Correção aqui
        {
            this.Descricao = descricao;
        }

        public abstract void QuandoPegada(Jogador jogador);
    }
}