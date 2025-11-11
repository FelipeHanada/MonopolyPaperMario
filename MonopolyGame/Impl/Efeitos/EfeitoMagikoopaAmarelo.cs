using MonopolyGame.Utils;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Exceptions;

namespace MonopolyGame.Impl.Efeitos;

/// <summary>
/// Efeito temporário ativado pelo Magikoopa Amarelo.
/// No próximo turno, o jogador que ativou este efeito (jogador)
/// coleta 100 moedas de cada jogador ativo (não falido) na partida.
/// </summary>
/// 
public class EfeitoMagikoopaAmarelo : IEfeitoJogador
{
    public void Aplicar(Jogador jogador)
    {
        Log.WriteLine($"--- Efeito Magikoopa Amarelo ativado para {jogador.Nome} ---");
        jogador.Partida.AdicionarRegistro($"--- Efeito Magikoopa Amarelo ativado para {jogador.Nome} ---");

        var partida = jogador.Partida;
        
        // Filtra todos os outros jogadores que não estão falidos
        var jogadoresPagadores = partida.Jogadores
            .Where(j => j != jogador && !j.Falido)
            .ToList();
        
        int valorPorJogador = 100;
        int totalColetado = 0;

        foreach (var pagador in jogadoresPagadores)
        {
            try
            {
                // USANDO TRANSFERIRDINHEIROPARA: O pagador paga 100 moedas ao jogador (o receptor).
                // O TransferirDinheiroPara já lida com o Debitar no pagador e Creditar no jogador.
                pagador.TransferirDinheiroPara(jogador, valorPorJogador);
                totalColetado += valorPorJogador;
                Log.WriteLine($"- {pagador.Nome} pagou {valorPorJogador} moedas a {jogador.Nome}.");
                jogador.Partida.AdicionarRegistro($"- {pagador.Nome} pagou {valorPorJogador} moedas a {jogador.Nome}.");
            }
            catch (FundosInsuficientesException)
            {
                // Captura a exceção se o jogador não tiver fundos
                // O jogo deve forçar o jogador a resolver a dívida (hipotecar, vender, etc.)
                Log.WriteLine($"- AVISO: {pagador.Nome} não conseguiu pagar {valorPorJogador} a {jogador.Nome} e precisa resolver dívidas ou declarar falência.");
                jogador.Partida.AdicionarRegistro($"- AVISO: {pagador.Nome} não conseguiu pagar {valorPorJogador} a {jogador.Nome} e precisa resolver dívidas ou declarar falência.");
                
                // Em um jogo real, aqui entraria a lógica de Negociação/Falência.
                // Por enquanto, apenas avisamos e o jogo continua com a dívida pendente (se a regra permitir)
                // ou força a falência imediata.
            }
        }

        if (totalColetado > 0)
        {
            Log.WriteLine($"{jogador.Nome} coletou um total de {totalColetado} moedas dos outros jogadores. Saldo atual: {jogador.Dinheiro}");
            jogador.Partida.AdicionarRegistro($"{jogador.Nome} coletou um total de {totalColetado} moedas dos outros jogadores. Saldo atual: {jogador.Dinheiro}");
        }

        Log.WriteLine($"--- Fim do Efeito ---");
        jogador.Partida.AdicionarRegistro($"--- Fim do Efeito ---");
    }
}
