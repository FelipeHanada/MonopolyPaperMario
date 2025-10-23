using MonopolyPaperMario.MonopolyGame.Model;

namespace MonopolyGame.impl.Cartas
{
    internal class CartaTimeout : CartaSorte
    {
        public CartaTimeout() : base("Kevlar ativou seu poder timeout. Você ficará uma rodada sem jogar.", new ReverterPodeJogar())
        {
            
        }
        public override void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"Sorte: {Descricao}");
            Efeito?.Execute(jogador);
            Console.WriteLine("================DEBUG=================\nAgendando reversão do efeito da carta.");
            Partida.GetPartida().addEfeitoTurnoParaJogadores(1, new ReverterPodeJogar(), [jogador]);
        }
    }
}
