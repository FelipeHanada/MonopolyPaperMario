using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Impl; // To access EfeitoDuplighost
using System;
using System.Linq; // Keep imports clean

// Use consistent namespace
namespace MonopolyPaperMario.MonopolyGame.Impl.Cartas
{
    internal class CartaDuplighost : CartaSorte
    {
        // 1. O construtor injeta o EfeitoDuplighost real, que fará todo o trabalho.
        public CartaDuplighost() : base(
            "Duplighost apareceu! Ele se transformará em um jogador aleatório e pagará sua próxima despesa de propriedade/aluguel.", 
            new EfeitoDuplighost(Partida.GetPartida())
        )
        {

        }
        
        public override void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"Sorte: {Descricao}");
            
            // 2. Apenas executa o efeito.
            // O EfeitoDuplighost.Execute() cuidará de:
            //    a) Sortear o alvo.
            //    b) Criar o EfeitoDuplighostReversor.
            //    c) Agendar o EfeitoDuplighostReversor (que é o 'flag' no jogador).
            Efeito?.Execute(jogador);
            
            // Removemos a linha de debug e o agendamento duplicado, pois isso é feito pelo Efeito.
        }
    }
}