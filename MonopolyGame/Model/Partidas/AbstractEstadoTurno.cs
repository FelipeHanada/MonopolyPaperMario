using MonopolyGame.Interface;
using MonopolyGame.Interface.Partidas;
using MonopolyGame.Model.Leiloes;
using MonopolyGame.Model.PossesJogador;
using MonopolyGame.Model.PropostasTroca;

namespace MonopolyGame.Model.Partidas;


public abstract class AbstractEstadoTurno : IEstadoTurno
{
    public abstract EstadoTurnoId EstadoId { get; }
    public abstract Jogador JogadorAtual { get; }
    public virtual bool RolarDados(out (int, int) dados, out int posicaoFinal) { throw new NotImplementedException("Ação RolarDados não implementada ou inválida neste estado."); }
    public virtual bool HipotecarPropriedade(Propriedade propriedade) { throw new NotImplementedException("Ação HipotecarPropriedade não implementada ou inválida neste estado."); }
    public virtual bool MelhorarImovel(Imovel imovel) { throw new NotImplementedException("Ação MelhorarImovel não implementada ou inválida neste estado."); }
    public virtual bool DepreciarImovel(Imovel imovel) { throw new NotImplementedException("Ação DepreciarImovel não implementada ou inválida neste estado."); }
    public virtual bool PodeRolarDados() { return false; }
    public virtual bool PodeEncerrarTurno() { return false; }

    public virtual Leilao GetLeilao() { throw new NotImplementedException("Ação GetLeilao não implementada ou inválida neste estado."); }
    public virtual Jogador GetJogadorAtualLeilao() { throw new NotImplementedException("Ação GetJogadorAtualLeilao não implementada ou inválida neste estado."); }
    public virtual Leilao IniciarLeilao(IPosseJogador posseJogador) { throw new NotImplementedException("Ação IniciarLeilao não implementada ou inválida neste estado."); }
    public virtual Jogador DarLanceLeilao(int delta) { throw new NotImplementedException("Ação DarLanceLeilao não implementada ou inválida neste estado."); }
    public virtual Jogador DesistirLeilao() { throw new NotImplementedException("Ação DesistirLeilao não implementada ou inválida neste estado."); }

    public virtual PropostaTroca GetPropostaVenda() { throw new NotImplementedException("Ação GetPropostaVenda não implementada ou inválida neste estado."); }
    public virtual PropostaTroca IniciarPropostaVenda() { throw new NotImplementedException("Ação IniciarPropostaVenda não implementada ou inválida neste estado."); }
    public virtual void EncerrarPropostaVenda(bool aceite) { throw new NotImplementedException("Ação EncerrarPropostaVenda não implementada ou inválida neste estado."); }
}
