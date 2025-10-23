using System.Collections.Generic;
using MonopolyPaperMario.MonopolyGame.Interface;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class PropostaDeTroca
    {
        public Jogador Ofertante { get; }
        public Jogador Alvo { get; }
        public List<IPosseJogador> PossesDoOfertante { get; }
        public List<IPosseJogador> PossesDoAlvo { get; }
        public int ValorEmDinheiro { get; } // Positivo = Ofertante paga Alvo. Negativo = Alvo paga Ofertante.

        public PropostaDeTroca(Jogador ofertante, Jogador alvo, List<IPosseJogador> possesDoOfertante, List<IPosseJogador> possesDoAlvo, int valorEmDinheiro)
        {
            Ofertante = ofertante;
            Alvo = alvo;
            PossesDoOfertante = possesDoOfertante;
            PossesDoAlvo = possesDoAlvo;
            ValorEmDinheiro = valorEmDinheiro;
        }
    }
}