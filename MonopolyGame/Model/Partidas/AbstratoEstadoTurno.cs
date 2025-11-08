using MonopolyGame.Interface;
using MonopolyGame.Interface.Partidas;
using MonopolyGame.Model.Leiloes;
using MonopolyGame.Model.PossesJogador;
using MonopolyGame.Model.PropostasTroca;

namespace MonopolyGame.Model.Partidas;


public abstract class AbstratoEstadoTurno : IEstadoTurno
{
    public abstract EstadoTurnoId EstadoId { get; }
    public abstract Jogador JogadorAtual { get; }
    public virtual bool PodeRolarDados { get; } = false;
    public virtual bool PodeEncerrarTurno { get; } = false;
    public virtual bool PodeIniciarPropostaTroca { get; } = false;

    public virtual List<(int, int)> GetDadosRolados() { throw new NotImplementedException("Ação GetDadosRolados não implementada ou inválida neste estado."); }
    public virtual bool UsarPasseLivreDaCadeia() { throw new NotImplementedException("Ação UsarPasseLivreDaCadeia não implementada ou inválida neste estado."); }
    public virtual bool RolarDados(out (int, int) dados, out int posicaoFinal) { throw new NotImplementedException("Ação RolarDados não implementada ou inválida neste estado."); }
    public virtual bool HipotecarPropriedade(Propriedade propriedade) { throw new NotImplementedException("Ação HipotecarPropriedade não implementada ou inválida neste estado."); }
    public virtual bool MelhorarImovel(Imovel imovel) { throw new NotImplementedException("Ação MelhorarImovel não implementada ou inválida neste estado."); }
    public virtual bool DepreciarImovel(Imovel imovel) { throw new NotImplementedException("Ação DepreciarImovel não implementada ou inválida neste estado."); }

    public virtual Leilao Leilao { get => throw new NotImplementedException("Leilao não implementada ou inválida neste estado."); }
    public virtual Jogador DarLanceLeilao(int delta) { throw new NotImplementedException("Ação DarLanceLeilao não implementada ou inválida neste estado."); }
    public virtual Jogador DesistirLeilao() { throw new NotImplementedException("Ação DesistirLeilao não implementada ou inválida neste estado."); }

    public virtual PropostaTroca PropostaTroca { get => throw new NotImplementedException("PropostaTroca não implementada ou inválida neste estado."); }
    public virtual PropostaTroca IniciarPropostaTroca() { throw new NotImplementedException("Ação IniciarPropostaTroca não implementada ou inválida neste estado."); }
    public virtual void EncerrarPropostaTroca(bool aceite) { throw new NotImplementedException("Ação EncerrarPropostaTroca não implementada ou inválida neste estado."); }
}
