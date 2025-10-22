using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class CartaSorte : CartaInstantanea
    {
        private readonly IEfeitoJogador efeito;

        public CartaSorte(string descricao, IEfeitoJogador efeito) : base(descricao) // Correção aqui
        {
            this.efeito = efeito;
        }

        public override void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"Sorte: {Descricao}");
            efeito?.Execute(jogador);
        }
    }
}