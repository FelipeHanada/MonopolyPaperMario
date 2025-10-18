using MonopolyPaperMario.Interface;

namespace MonopolyPaperMario.Impl
{
    public class EfeitoIrParaCadeia : IEfeitoJogador
    {
        public void Execute(Player jogador)
        {
            jogador.SetPreso(true);
            // jogador.MoverParaPosicao(10); // posição da cadeia
        }

    }
}