using MonopolyGame.Interface.Controles;
using MonopolyGame.Model.Leiloes;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PossesJogador;
using MonopolyGame.Model.PropostasTroca;

namespace MonopolyGame.Impl.Controles;

public class ControlePartida(Partida partida) : IControlePartida
{
    public Partida Partida { get; } = partida;

    public bool ComandoDisponivel(ControlePartidaComando comando)
    {
        return false;
    }

    public List<(int, int)> Comum_GetDadosRolados()
    {
        return Partida.EstadoTurnoAtual.GetDadosRolados();
    }
    public bool Comum_RolarDados()
    {
        return Partida.EstadoTurnoAtual.RolarDados(out _, out _);
    }
    public bool Comum_HipotecarPropriedade(Propriedade propriedade)
    {
        return Partida.EstadoTurnoAtual.HipotecarPropriedade(propriedade);
    }
    public bool Comum_MelhorarImovel(Imovel imovel)
    {
        return Partida.EstadoTurnoAtual.MelhorarImovel(imovel);
    }
    public bool Comum_DepreciarImovel(Imovel imovel)
    {
        return Partida.EstadoTurnoAtual.DepreciarImovel(imovel);
    }
    public bool Comum_EncerrarTurno()
    {
        if (!Partida.EstadoTurnoAtual.PodeEncerrarTurno) return false;
        Partida.FinalizarTurno();
        return true;
    }
    public bool Comum_UsarPasseDaCadeia()
    {
        return Partida.EstadoTurnoAtual.UsarPasseLivreDaCadeia();
    }

    public Jogador Leilao_GetJogadorAtual()
    {
        throw new NotImplementedException("Este método ainda não foi implementado.");
    }
    public bool Leilao_Iniciar(Leilao leilao)
    {
        throw new NotImplementedException("Este método ainda não foi implementado.");
    }
    public bool Leilao_DarLance(int delta)
    {
        throw new NotImplementedException("Este método ainda não foi implementado.");
    }
    public bool Leilao_Desistir()
    {
        throw new NotImplementedException("Este método ainda não foi implementado.");
    }

    public bool Troca_Iniciar(PropostaTroca propostaTroca)
    {
        if (!Partida.EstadoTurnoAtual.PodeIniciarPropostaTroca) return false;
        Partida.IniciarPropostaTroca(propostaTroca);
        return true;
    }
    public PropostaTroca Troca_GetPropostaTroca()
    {
        return Partida.EstadoTurnoAtual.PropostaTroca;
    }
    public Jogador? Troca_GetJogadorProponente()
    {
        return Partida.EstadoTurnoAtual.PropostaTroca.Ofertante;
    }
    public Jogador Troca_GetJogadorDestinatario()
    {
        return Partida.EstadoTurnoAtual.PropostaTroca.Destinatario;
    }
    public bool Troca_Aceitar()
    {
        return Partida.EncerrarPropostaTroca(true);
    }
    public bool Troca_Recusar()
    {
        return Partida.EncerrarPropostaTroca(false);
    }
}
