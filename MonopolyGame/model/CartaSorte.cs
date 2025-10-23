using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class CartaSorte : CartaInstantanea
    {
        protected  IEfeitoJogador Efeito{get; }

        public CartaSorte(string descricao, IEfeitoJogador efeito) : base(descricao) // Correção aqui
        {
            this.Efeito = efeito;
        }

        public override void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"Sorte: {Descricao}");
            Efeito?.Execute(jogador);
        }
    }
}