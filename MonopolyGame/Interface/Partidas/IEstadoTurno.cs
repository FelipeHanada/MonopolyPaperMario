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
    List<(int, int)> GetDadosRolados();
    bool RolarDados(out (int, int) dados, out int posicaoFinal);
    bool UsarPasseLivreDaCadeia();
    bool HipotecarPropriedade(Propriedade propriedade);
    bool MelhorarImovel(Imovel imovel);
    bool DepreciarImovel(Imovel imovel);
    // ------------------------------------------------------
    Leilao Leilao { get; }
    Jogador? JogadorAtualLeilao { get; }
    bool DarLanceLeilao(int aumento);
    bool DesistirLeilao();
    // ------------------------------------------------------
    PropostaTroca PropostaTroca { get; }
    bool EncerrarPropostaTroca(bool aceite);
    //PropostaTroca GetPropostaTroca();
};
