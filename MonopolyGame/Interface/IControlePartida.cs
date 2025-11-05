using MonopolyGame.Model.Leiloes;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PossesJogador;

namespace MonopolyGame.Interface;

/*
 * + getEstado(): EstadoTurnoFlag
 * ---------------------------------------------
 * + rolarDados(): bool
 * + hipotecarPropriedade(Propriedade): bool
 * + melhorarImóvel(Imóvel): bool
 * + depreciarImóvel(Imóvel): bool
 * + FinalizarTurno(): Jogador
 * ---------------------------------------------
 * + getLeilão(): Leilão
 * + getJogadorAtualLeilão(): Jogador
 * + iniciarLeilão(IPosseJogador): Leilão
 * + darLanceLeilão(int delta): Jogador
 * + desistirLeilão(): Jogador
 * ---------------------------------------------
 * + getPropostaTroca(): PropostaTroca
 * + iniciarPropostaTroca(): PropostaTroca
 * + encerrarPropostaTroca(bool aceite)
 */

interface IControlePartida
{
    Partida GetPartida();
    void Reset();

    //EstadoTurnoFlag getEstado();
    //bool rolarDados();
    bool HipotecarPropriedade(Propriedade propriedade);
    bool MelhorarImovel(Imovel imovel);
    bool DeprecriarImovel(Imovel imovel);
    Jogador FinalizarTurno();
    Leilao? GetLeilao();
    Jogador GetJogadorAtualLeilao();
    Leilao IniciarLeilao(IPosseJogador posse);
    void DarLanceLeilao(int delta);
    void DesistirLeilao();
    //PropostaTroca? GetPropostaTroca();
    //PropostaTroca IniciarPropostaTroca();
    void EncerrarPropostaTroca(bool aceite);
}
