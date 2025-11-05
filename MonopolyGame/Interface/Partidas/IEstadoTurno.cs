using MonopolyGame.Model.Leiloes;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PossesJogador;
using MonopolyGame.Model.PropostasTroca;

namespace MonopolyGame.Interface.Partidas;


public enum EstadoTurnoId
{
    Comum,
    Leilao,
    PropostaTroca,
    Encerrado,
};


public interface IEstadoTurno
{
    EstadoTurnoId EstadoId { get; }
    Jogador JogadorAtual { get; }

    // ------------------------------------------------------
    bool PodeRolarDados { get; }
    bool PodeEncerrarTurno { get; }
    bool PodeIniciarPropostaTroca { get; }
    // ------------------------------------------------------
    bool RolarDados(out (int, int) dados, out int posicaoFinal);
    bool HipotecarPropriedade(Propriedade propriedade);
    bool MelhorarImovel(Imovel imovel);
    bool DepreciarImovel(Imovel imovel);
    // ------------------------------------------------------
    //Leilao Leilao { get; }
    //Jogador JogadorAtualLeilao { get; }
    //Jogador DarLanceLeilao(int aumento);
    //Jogador DesistirLeilao();
    // ------------------------------------------------------
    PropostaTroca PropostaTroca { get; }
    void EncerrarPropostaTroca(bool aceite);
    //PropostaTroca GetPropostaTroca();
};
