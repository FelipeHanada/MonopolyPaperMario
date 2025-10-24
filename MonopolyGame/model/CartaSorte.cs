using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class CartaSorte : CartaInstantanea
    {
        protected  IEfeitoJogador? Efeito{get; }

        // CORREÇÃO: Permite que o parâmetro 'efeito' seja nulo usando '?'
        public CartaSorte(string descricao, IEfeitoJogador? efeito) : base(descricao)
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