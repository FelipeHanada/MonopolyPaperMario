using MonopolyGame.Utils;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Model.Tabuleiros;

public class Tabuleiro(Piso[] pisos, List<Jogador> jogadores)
{
    public Piso[] Pisos { get; } = pisos;
    public List<PosicaoJogador> PosicoesJogadores { get; } = [.. jogadores.Select(jogador => new PosicaoJogador(jogador, 0))];
    
    public void SetPiso(int i, Piso piso)
    {
        Pisos[i] = piso;
    }

    public int GetPosicao(Jogador jogador)
    {
        return PosicoesJogadores.FirstOrDefault(p => p.Jogador == jogador)?.PosicaoAtual ?? -1;
    }

    public void MoveJogador(Jogador jogador, int offset)
    {
        if (jogador.Reverso) // para fazer a carta Groove Guy tonto funcionar
        {
            offset = -offset;
        }
        if (jogador.Multiplicador != 0)
        {
            offset = offset * jogador.Multiplicador;
            Log.WriteLine("O jogador teve o valor dos dados multiplicado por "+jogador.Multiplicador+". Novo valor é "+offset+".");
        }
        PosicaoJogador? posAtual = PosicoesJogadores.FirstOrDefault(p => p.Jogador == jogador);
        if (posAtual == null) return;

        int posAnterior = posAtual.PosicaoAtual;
        int novaPosicao = (Pisos.Length + (posAnterior + offset) % Pisos.Length) % Pisos.Length;

        if (novaPosicao < posAnterior && offset > 0)
        {
            Log.WriteLine($"{jogador.Nome} passou pelo Ponto de Partida e coletou $200!");
            jogador.Creditar(200);
        }

        posAtual.PosicaoAtual = novaPosicao;
        Piso pisoAtual = Pisos[novaPosicao];

        Log.WriteLine($"{jogador.Nome} moveu-se para a casa {novaPosicao}: '{pisoAtual.Nome}'.");
        pisoAtual.Efeito(jogador);
    }

    public void MoverJogadorPara(Jogador jogador, int posicao, bool coletarSalario)
    {
        PosicaoJogador? posAtual = PosicoesJogadores.FirstOrDefault(p => p.Jogador == jogador);
        if (posAtual == null) return;

        if (coletarSalario && posicao < posAtual.PosicaoAtual)
        {
            Log.WriteLine($"{jogador.Nome} passou pelo Ponto de Partida e coletou $200!");
            jogador.Creditar(200);
        }

        posAtual.PosicaoAtual = posicao;
        Log.WriteLine($"{jogador.Nome} foi movido para a casa {posicao}: '{Pisos[posicao].Nome}'.");
    }

    public void TrocarPosicao(Jogador jogadorA, Jogador jogadorB)
    {
        PosicaoJogador? posA = PosicoesJogadores.FirstOrDefault(p => p.Jogador == jogadorA);
        PosicaoJogador? posB = PosicoesJogadores.FirstOrDefault(p => p.Jogador == jogadorB);

        if (posA == null || posB == null)
        {
            Log.WriteLine("Erro na troca de posição: Um dos jogadores não está registrado no tabuleiro.");
            return;
        }

        // 1. Salva as posições atuais
        int posicaoOriginalA = posA.PosicaoAtual;
        int posicaoOriginalB = posB.PosicaoAtual;

        // 2. Troca as posições
        posA.PosicaoAtual = posicaoOriginalB;
        posB.PosicaoAtual = posicaoOriginalA;

        Log.WriteLine($"POSIÇÃO TROCADA: {jogadorA.Nome} está agora em {posicaoOriginalB}. {jogadorB.Nome} está agora em {posicaoOriginalA}.");

        // Em uma troca de posição, os efeitos do piso de destino não são ativados.
    }
    
    // Dentro de namespace MonopolyGame.Model > class Tabuleiro


    public void RotacionarPosicoesJogadores()
    {
        // 1. Garante que há mais de um jogador para fazer a rotação.
        if (PosicoesJogadores.Count <= 1)
        {
            Log.WriteLine("[AVISO] Rotação de posições ignorada: Menos de dois jogadores.");
            return;
        }

    // 2. Cria uma lista temporária das POSIÇÕES ATUAIS dos jogadores
    // na ordem em que estão na lista 'PosicoesJogadores'.
        List<int> posicoesOriginais = PosicoesJogadores.Select(p => p.PosicaoAtual).ToList();

        int totalJogadores = posicoesOriginais.Count;

        Log.WriteLine("Iniciando rotação de posições dos jogadores...");

    // 3. Aplica a Rotação: 
    // O jogador no índice 'i' recebe a posição do jogador no índice '(i + 1) % totalJogadores'.
    for (int i = 0; i < totalJogadores; i++)
    {
    // Calcula o índice do jogador vizinho (N+1, com wrap-around para o primeiro)
        int indiceNovoDonoDaPosicao = (i + 1) % totalJogadores;
    
    // A nova posição do jogador 'i' é a posição original do jogador 'i + 1'.
        int novaPosicao = posicoesOriginais[indiceNovoDonoDaPosicao];
    
        // Atualiza a PosicaoJogador.
        PosicaoJogador posJogadorAtual = PosicoesJogadores[i];
    
        Log.WriteLine($"- {posJogadorAtual.Jogador.Nome} (antiga {posJogadorAtual.PosicaoAtual}) vai para a posição {novaPosicao} (posição do {PosicoesJogadores[indiceNovoDonoDaPosicao].Jogador.Nome})");
    
        posJogadorAtual.PosicaoAtual = novaPosicao;
}

    // NOTA: Os efeitos dos Pisos não são acionados durante a rotação.
    Log.WriteLine("Rotação de posições concluída.");
}
    
}