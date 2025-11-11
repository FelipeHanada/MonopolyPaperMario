using MonopolyGame.Interface.Partidas;
using MonopolyGame.Model.PossesJogador;

namespace MonopolyGame.Model.Partidas;


public class EstadoTurnoComum(Jogador jogadorAtual) : AbstratoEstadoTurno
{
    private readonly Random dado = new();
    public override EstadoTurnoId EstadoId { get; } = EstadoTurnoId.Comum;
    public override Jogador JogadorAtual { get; } = jogadorAtual;

    public override bool PodeRolarDados { get => (Rolagem < LimiteRolagems && RolagemIgual); }
    public override bool PodeEncerrarTurno { get => !PodeRolarDados || !JogadorAtual.PodeJogar; }
    public override bool PodeIniciarPropostaTroca { get; } = true;
    public int LimiteRolagems = 3;

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

        JogadorAtual.Partida.AdicionarRegistro($"Valor Total dos dados: {dados.Item1} + {dados.Item2} = {totalDados}");

        if (RolagemIgual && !JogadorAtual.Preso && Rolagem != LimiteRolagems)
        {
            JogadorAtual.Partida.AdicionarRegistro($"Dados iguais, Jogue de novo!");
        }

        if (JogadorAtual.Preso)
        {
            if (RolagemIgual)
            {
                JogadorAtual.Preso = false;
                LimiteRolagems++;
            }

            JogadorAtual.Partida.AdicionarRegistro($"{JogadorAtual.Nome} saiu da prisão por tirar dados iguais");
            return true;
        }

        if (RolagemIgual && Rolagem == LimiteRolagems)
        {
            JogadorAtual.Preso = true;
            JogadorAtual.Partida.Tabuleiro.MoverJogadorPara(JogadorAtual, 10, false);
            JogadorAtual.Partida.AdicionarRegistro($"{JogadorAtual.Nome} foi para a prisão");
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

        JogadorAtual.Partida.AdicionarRegistro($"{JogadorAtual.Nome} usou um passe livre da prisão");
        return true;
    }
    public override bool HipotecarPropriedade(Propriedade propriedade)
    {
        if (propriedade.Proprietario != JogadorAtual || propriedade.Hipotecada) return false;
        propriedade.Hipotecada = true;
        JogadorAtual.Dinheiro += propriedade.ValorHipoteca;

        JogadorAtual.Partida.AdicionarRegistro($"{JogadorAtual.Nome} hipotecou {propriedade.Nome}");
        return true;
    }
    public override bool MelhorarImovel(Imovel imovel)
    {
        if (imovel.Proprietario != JogadorAtual || JogadorAtual.Dinheiro < imovel.PrecoComprarCasa) return false;
        if (imovel.AdicionarCasa())
        {
            JogadorAtual.Dinheiro -= imovel.PrecoComprarCasa;

            JogadorAtual.Partida.AdicionarRegistro($"{JogadorAtual.Nome} melhorou {imovel.Nome}");
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

            JogadorAtual.Partida.AdicionarRegistro($"{JogadorAtual.Nome} depreciou {imovel.Nome}");
            return true;
        }
        return false;
    }
}
