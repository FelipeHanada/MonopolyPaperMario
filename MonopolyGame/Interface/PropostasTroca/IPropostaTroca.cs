using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Interface.PropostasTroca;


public interface IPropostaTroca
{
    public Jogador? Ofertante { get; }
    public Jogador Destinatario { get; }
    public List<IPosseJogador> PossesOfertadas { get; }
    public List<IPosseJogador> PossesDesejadas { get; }
    public int DinheiroOfertado { get; }
    bool Valido(); // checa se os jogadores possuem as posses e se tem dinheiro suficiente
    bool Efetuar();
}
