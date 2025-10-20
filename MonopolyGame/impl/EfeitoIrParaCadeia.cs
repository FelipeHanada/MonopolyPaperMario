using MonopolyPaperMario.Interface;
using MonopolyPaperMario.model;

namespace MonopolyPaperMario.Impl
{

    public class EfeitoIrParaCadeia : IEfeitoJogador
    {
        public void Execute(Jogador jogador)
        {
            jogador.SetPreso(true);
            // jogador.MoverParaPosicao(10); // posição da cadeia
        }

    }
}