using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;

namespace MonopolyGame.Impl.Efeitos;


public class EfeitoDuplighost : IEfeitoJogador
{
    public void Aplicar(Jogador jogador)
    {
        Console.WriteLine("\n------------------------------------------------");
        Console.WriteLine($"!!! {jogador.Nome} sacou DUPLIGHOST !!!");

        // 1. Seleciona o 'Duplighost' (o jogador que pagará a próxima conta)

        // Filtra jogadores ativos, EXCLUINDO o jogador que sacou a carta (para ser mais interessante)
        var jogadoresAtivos = jogador.Partida.Jogadores.Where(j => !j.Falido && j != jogador).ToList();

        if (jogadoresAtivos.Count == 0)
        {
            Console.WriteLine("Não há outros jogadores ativos para o Duplighost se transformar. Efeito cancelado.");
            return;
        }

        // Usa um gerador de números aleatórios para escolher o alvo
        Random random = new Random();
        int indexDuplighost = random.Next(jogadoresAtivos.Count);
        Jogador duplighostAlvo = jogadoresAtivos[indexDuplighost];

        Console.WriteLine($"Duplighost transformou-se em {duplighostAlvo.Nome}! Ele pagará a sua próxima despesa de propriedade/aluguel.");

        // 2. Cria o Efeito Agendado de Reversão
        IEfeitoJogador efeitoReversor = new EfeitoDuplighostReversor(jogador.Partida, duplighostAlvo);

        // 3. Agenda a reversão para daqui a 1 turno (dura apenas o próximo turno do jogador)
        // O efeito deve se aplicar APENAS ao jogador que sacou a carta.
        jogador.Partida.addEfeitoTurnoParaJogadores(
            1, // Duração de 1 turno (será removido no início do turno seguinte)
            efeitoReversor,
            new Jogador[] { jogador } // O efeito será executado sobre o jogador que sacou a carta
        );

        Console.WriteLine("------------------------------------------------\n");
    }
}
