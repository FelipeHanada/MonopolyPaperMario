using MonopolyGame.Interface.Partidas;
using MonopolyGame.Model.Leiloes;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PossesJogador;
using MonopolyGame.Model.PropostasTroca;

namespace MonopolyGame.Interface.Controles;


//public interface IControlePartidaComandoArgumentParserStrategy<T>
//{
//    T Parse(object[] args);
//}

//public class ControlePartidaComandoArgumentParserStrategyNone : IControlePartidaComandoArgumentParserStrategy<object>
//{
//    public object Parse(object[] args)
//    {
//        return new object();
//    }
//}

//public class ControlePartidaComandoSingleArgumentParserStrategy<T> : IControlePartidaComandoArgumentParserStrategy<T>
//{
//    public T Parse(object[] args)
//    {
//        return (T)args[0];
//    }
//}

public enum ControlePartidaComando
{
    Comum_GetDadosRolados,
    Comum_RolarDados,
    Comum_HipotecarPropriedade,
    Comum_MelhorarImovel,
    Comum_DepreciarImovel,
    Comum_EncerrarTurno,
    Comum_UsarPasseDaCadeia,
    Comum_IniciarLeilao,
    Comum_IniciarPropostaTroca,

    Leilao_GetJogadorAtual,
    Leilao_DarLance,
    Leilao_Desistir,

    Troca_Aceitar,
    Troca_Recusar,
}

//public interface IControlePartida
//{
//    Partida Partida { get; }
//    bool PodeExecutar(ControlePartidaComando comando);
//    bool ExecutarComando(ControlePartidaComando comando, object[] args);
//}


public interface IControlePartida
{
    Partida Partida { get; }
    bool ComandoDisponivel(ControlePartidaComando comando);
    
    List<(int, int)> Comum_GetDadosRolados();
    bool Comum_RolarDados();
    bool Comum_HipotecarPropriedade(Propriedade propriedade);
    bool Comum_MelhorarImovel(Imovel imovel);
    bool Comum_DepreciarImovel(Imovel imovel);
    bool Comum_EncerrarTurno();
    bool Comum_UsarPasseDaCadeia();
    Jogador Leilao_GetJogadorAtual();
    bool Leilao_Iniciar(Leilao leilao);
    bool Leilao_DarLance(int delta);
    bool Leilao_Desistir();
    bool Troca_Iniciar(PropostaTroca propostaTroca);
    PropostaTroca Troca_GetPropostaTroca();
    Jogador? Troca_GetJogadorProponente();
    Jogador Troca_GetJogadorDestinatario();
    bool Troca_Aceitar();
    bool Troca_Recusar();
}
