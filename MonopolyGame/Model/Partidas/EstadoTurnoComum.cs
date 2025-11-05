using MonopolyGame.Interface.Partidas;
using MonopolyGame.Model.PossesJogador;

namespace MonopolyGame.Model.Partidas;


public class EstadoTurnoComum(Jogador jogadorAtual) : AbstractEstadoTurno
{
    private readonly Random dado = new();
    public override EstadoTurnoId EstadoId { get; } = EstadoTurnoId.Comum;
    public override Jogador JogadorAtual { get; } = jogadorAtual;
    
    public int Rolagem { get; private set; } = 0;
    public bool RolagemIgual { get; private set; } = true;
    
    public override bool PodeRolarDados()
    {
        return Rolagem < 3 && RolagemIgual;
    }
    public override bool RolarDados(out (int, int) dados, out int posicaoFinal)
    {
        dados = (-1, -1);
        posicaoFinal = -1;
        if (!PodeRolarDados()) return false;

        Rolagem++;

        dados = (dado.Next(1, 7), dado.Next(1, 7));
        RolagemIgual = dados.Item1 == dados.Item2;
        int totalDados = dados.Item1 + dados.Item2;

        if (JogadorAtual.Preso)
        {
            if (RolagemIgual)
            {
                JogadorAtual.Preso = false;
                RolagemIgual = false;
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
    public override bool PodeEncerrarTurno()
    {
        return !PodeRolarDados() || !JogadorAtual.PodeJogar;
    }
}
