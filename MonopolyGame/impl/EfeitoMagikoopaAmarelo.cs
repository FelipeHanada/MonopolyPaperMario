using System;
using System.Linq;
using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyPaperMario.MonopolyGame.Exceptions; // Necessário para FundosInsuficientesException

namespace MonopolyPaperMario.MonopolyGame.Impl.Efeitos
{
    /// <summary>
    /// Efeito temporário ativado pelo Magikoopa Amarelo.
    /// No próximo turno, o jogador que ativou este efeito (jogadorAlvo)
    /// coleta 100 moedas de cada jogador ativo (não falido) na partida.
    /// </summary>
    public class EfeitoMagikoopaAmarelo : IEfeitoJogador
    {
        public string Nome => "Mágica do Magikoopa Amarelo";
        public string Descricao => "Coleta 100 Moedas de cada jogador ativo.";

        public void Execute(Jogador jogadorAlvo)
        {
            Console.WriteLine($"--- Efeito Magikoopa Amarelo ativado para {jogadorAlvo.Nome} ---");

            var partida = Partida.GetPartida();
            
            // Filtra todos os outros jogadores que não estão falidos
            var jogadoresPagadores = partida.Jogadores
                .Where(j => j != jogadorAlvo && !j.Falido)
                .ToList();
            
            int valorPorJogador = 100;
            int totalColetado = 0;

            foreach (var pagador in jogadoresPagadores)
            {
                try
                {
                    // USANDO TRANSFERIRDINHEIROPARA: O pagador paga 100 moedas ao jogadorAlvo (o receptor).
                    // O TransferirDinheiroPara já lida com o Debitar no pagador e Creditar no jogadorAlvo.
                    pagador.TransferirDinheiroPara(jogadorAlvo, valorPorJogador);
                    totalColetado += valorPorJogador;
                    Console.WriteLine($"- {pagador.Nome} pagou {valorPorJogador} moedas a {jogadorAlvo.Nome}.");
                }
                catch (FundosInsuficientesException)
                {
                    // Captura a exceção se o jogador não tiver fundos
                    // O jogo deve forçar o jogador a resolver a dívida (hipotecar, vender, etc.)
                    Console.WriteLine($"- AVISO: {pagador.Nome} não conseguiu pagar {valorPorJogador} a {jogadorAlvo.Nome} e precisa resolver dívidas ou declarar falência.");
                    
                    // Em um jogo real, aqui entraria a lógica de Negociação/Falência.
                    // Por enquanto, apenas avisamos e o jogo continua com a dívida pendente (se a regra permitir)
                    // ou força a falência imediata.
                }
            }

            if (totalColetado > 0)
            {
                Console.WriteLine($"{jogadorAlvo.Nome} coletou um total de {totalColetado} moedas dos outros jogadores. Saldo atual: {jogadorAlvo.Dinheiro}");
            }
            
            Console.WriteLine($"--- Fim do Efeito ---");
        }
    }
}