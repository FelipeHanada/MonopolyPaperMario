using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class CartaCofre : CartaInstantanea
    {
        private readonly IEfeitoJogador efeito;

        public CartaCofre(string descricao, IEfeitoJogador efeito) : base(descricao) // Correção aqui
        {
            this.efeito = efeito;
        }

        public override void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"Cofre: {Descricao}");
            efeito?.Execute(jogador);
        }
    }
}