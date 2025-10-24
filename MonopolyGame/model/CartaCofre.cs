using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class CartaCofre : CartaInstantanea
    {
        // CORREÇÃO: Padronizado para ser uma propriedade protegida, como em CartaSorte.
        protected IEfeitoJogador Efeito { get; }

        public CartaCofre(string descricao, IEfeitoJogador efeito) : base(descricao)
        {
            this.Efeito = efeito;
        }

        public override void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"Cofre: {Descricao}");
            Efeito?.Execute(jogador);
        }
    }
}