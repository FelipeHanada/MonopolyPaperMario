using MonopolyPaperMario.Interface;

namespace MonopolyPaperMario.Impl
{
    public class EfeitoSairDaCadeia : IEfeitoJogador
    {
        public void Execute(Player jogador)
        {
            jogador.SetPreso(false);

        }
    }
}