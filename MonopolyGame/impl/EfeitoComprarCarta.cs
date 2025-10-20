using MonopolyPaperMario.Interface; // Para usar IPlayerEffect
using MonopolyPaperMario.Model;    // Para usar a classe Player
using System;

namespace MonopolyPaperMario.Impl
{
 public class EfeitoComprarCarta : IEfeitoJogador
{
    private int quantidade = 1;

        private Propriedade propriedade;
        private int valor = propriedade.getDinheiro;

        public void Execute(Jogador jogador)
        {
            jogador.transferirDinheiroPara(jogador, valor);
        }

}   

}

