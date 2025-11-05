using MonopolyGame.Model.Leiloes;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PossesJogador;
using MonopolyGame.Model.PropostasTroca;

namespace MonopolyGame.Interface.Partidas;


public enum EstadoTurnoId
{
    Comum,
    Leilao,
    PropostaVenda,
    Encerrado,
};


public interface IEstadoTurno
{
    EstadoTurnoId EstadoId { get; }
    Jogador JogadorAtual { get; }
    // ------------------------------------------------------
    bool RolarDados(out (int, int) dados, out int posicaoFinal);
    bool HipotecarPropriedade(Propriedade propriedade);
    bool MelhorarImovel(Imovel imovel);
    bool DepreciarImovel(Imovel imovel);
    bool PodeEncerrarTurno();
    bool PodeRolarDados();
    // ------------------------------------------------------
    //Leilao Leilao { get; }
    //Jogador JogadorAtualLeilao { get; }
    //Jogador DarLanceLeilao(int aumento);
    //Jogador DesistirLeilao();
    // ------------------------------------------------------
    //PropostaTroca GetPropostaVenda();
};
