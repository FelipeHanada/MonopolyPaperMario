using MonopolyGame.Interface.Controles;
using MonopolyGame.Interface.Partidas;
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
        return comando switch
        {
            ControlePartidaComando.Comum_GetDadosRolados => Partida.EstadoTurnoAtual.EstadoId == EstadoTurnoId.Comum,
            ControlePartidaComando.Comum_RolarDados => (Partida.EstadoTurnoAtual.EstadoId == EstadoTurnoId.Comum && Partida.EstadoTurnoAtual.PodeRolarDados),
            ControlePartidaComando.Comum_HipotecarPropriedade => Partida.EstadoTurnoAtual.EstadoId == EstadoTurnoId.Comum,
            ControlePartidaComando.Comum_MelhorarImovel => Partida.EstadoTurnoAtual.EstadoId == EstadoTurnoId.Comum,
            ControlePartidaComando.Comum_DepreciarImovel => Partida.EstadoTurnoAtual.EstadoId == EstadoTurnoId.Comum,
            ControlePartidaComando.Comum_EncerrarTurno => Partida.EstadoTurnoAtual.PodeEncerrarTurno,
            ControlePartidaComando.Comum_UsarPasseDaCadeia => Partida.EstadoTurnoAtual.EstadoId == EstadoTurnoId.Comum,
            ControlePartidaComando.Comum_IniciarLeilao => Partida.EstadoTurnoAtual.EstadoId == EstadoTurnoId.Comum,
            ControlePartidaComando.Comum_IniciarPropostaTroca => Partida.EstadoTurnoAtual.EstadoId == EstadoTurnoId.Comum,

            ControlePartidaComando.Leilao_GetJogadorAtual => Partida.EstadoTurnoAtual.EstadoId == EstadoTurnoId.Leilao,
            ControlePartidaComando.Leilao_DarLance => Partida.EstadoTurnoAtual.EstadoId == EstadoTurnoId.Leilao,
            ControlePartidaComando.Leilao_Desistir => Partida.EstadoTurnoAtual.EstadoId == EstadoTurnoId.Leilao,

            ControlePartidaComando.Troca_Aceitar => Partida.EstadoTurnoAtual.EstadoId == EstadoTurnoId.PropostaTroca,
            ControlePartidaComando.Troca_Recusar => Partida.EstadoTurnoAtual.EstadoId == EstadoTurnoId.PropostaTroca,

            _ => false,
        };
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
