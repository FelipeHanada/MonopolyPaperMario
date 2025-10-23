using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;
// ... (outros using's)

namespace MonopolyPaperMario.MonopolyGame.Impl.Cartas
{
    // A classe CartaLavaVulcao parece ter sido colocada aqui por acidente,
    // mas focando na CartaPasseLivre:

    internal class CartaStarBeam : CartaSorte 
    {
        public CartaStarBeam() : base(
            "Passe Livre da Prisão. Esta carta pode ser guardada até que seja necessária ou vendida.", 
            null) // O efeito é nulo, pois a ação está em QuandoPegada
        {
        }

        public override void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"Sorte: {Descricao}");
            
            // Incrementa o contador do jogador (Isso está Perfeito)
            jogador.CartasPasseLivre++;
            
            Console.WriteLine($"{jogador.Nome} agora tem {jogador.CartasPasseLivre} carta(s) de Passe Livre.");
        }
    }
}