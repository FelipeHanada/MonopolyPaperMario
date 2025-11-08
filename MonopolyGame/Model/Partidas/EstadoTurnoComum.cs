using MonopolyGame.Interface.Partidas;
using MonopolyGame.Model.PossesJogador;

namespace MonopolyGame.Model.Partidas;


public class EstadoTurnoComum(Jogador jogadorAtual) : AbstratoEstadoTurno
{
    private readonly Random dado = new();
    public override EstadoTurnoId EstadoId { get; } = EstadoTurnoId.Comum;
    public override Jogador JogadorAtual { get; } = jogadorAtual;

    public override bool PodeRolarDados { get => Rolagem < 3 && RolagemIgual; }
    public override bool PodeEncerrarTurno { get => !PodeRolarDados || !JogadorAtual.PodeJogar; }
    public override bool PodeIniciarPropostaTroca { get; } = true;

    public List<(int, int)> DadosRolados { get; } = [];
    public int Rolagem { get => DadosRolados.Count; }
    public bool RolagemIgual { get => (DadosRolados.Count == 0) || (DadosRolados.Last().Item1 == DadosRolados.Last().Item2); }
   
    public override List<(int, int)> GetDadosRolados()
    {
        return DadosRolados;
    }

    public override bool RolarDados(out (int, int) dados, out int posicaoFinal)
    {
        dados = (-1, -1);
        posicaoFinal = -1;
        if (!PodeRolarDados) return false;

        dados = (dado.Next(1, 7), dado.Next(1, 7));
        DadosRolados.Add(dados);
        
        int totalDados = dados.Item1 + dados.Item2;

        if (JogadorAtual.Preso)
        {
            if (RolagemIgual)
            {
                JogadorAtual.Preso = false;
            }
            return true;
        }

        if (RolagemIgual && Rolagem == 3)
        {
            JogadorAtual.Preso = true;
            return true;
        }
        JogadorAtual.Partida.Tabuleiro.MoveJogador(JogadorAtual, totalDados);
        posicaoFinal = JogadorAtual.Partida.Tabuleiro.GetPosicao(JogadorAtual);
        return true;
    }
    public override bool UsarPasseLivreDaCadeia()
    {
        if (!JogadorAtual.Preso || JogadorAtual.CartasPasseLivre == 0) return false;
        JogadorAtual.CartasPasseLivre--;
        JogadorAtual.Preso = false;
        return true;
    }
    public override bool HipotecarPropriedade(Propriedade propriedade)
    {
        if (propriedade.Proprietario != JogadorAtual || propriedade.Hipotecada) return false;
        propriedade.Hipotecada = true;
        JogadorAtual.Dinheiro += propriedade.ValorHipoteca;
        return true;
    }
    public override bool MelhorarImovel(Imovel imovel)
    {
        if (imovel.Proprietario != JogadorAtual || JogadorAtual.Dinheiro < imovel.PrecoComprarCasa) return false;
        if (imovel.AdicionarCasa())
        {
            JogadorAtual.Dinheiro -= imovel.PrecoComprarCasa;
            return true;
        }
        return false;
    }
    public override bool DepreciarImovel(Imovel imovel)
    {
        if (imovel.Proprietario != JogadorAtual) return false;
        if (imovel.RemoverCasa())
        {
            JogadorAtual.Dinheiro += imovel.PrecoVenderCasa;
            return true;
        }
        return false;
    }
}
