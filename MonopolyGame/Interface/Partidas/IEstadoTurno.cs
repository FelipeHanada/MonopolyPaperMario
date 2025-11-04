using MonopolyGame.Model.Leiloes;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PossesJogador;

namespace MonopolyGame.Interface.Partidas;


public enum EstadoTurnoID
{
    Comum,
    Leilao,
    PropostaVenda,
    Encerrado,
};


public interface IEstadoTurno
{
    EstadoTurnoID GetEstado();

    bool RolarDados();
    bool HipotecarPropriedade(Propriedade propriedade);
    bool MelhorarImovel(Imovel imovel);
    bool DepreciarImovel(Imovel imovel);
    Jogador PassarAVez();

    Leilao GetLeilao();
    Jogador GetJogadorAtualLeilao();
    Leilao IniciarLeilao(IPosseJogador posseJogador);
    Jogador DarLanceLeilao(int delta);
    Jogador DesistirLeilao();

    //PropostaVenda GetPropostaVenda();
    //PropostaVenda IniciarPropostaVenda();
    //void EncerrarPropostaVenda(bool aceite);
};
