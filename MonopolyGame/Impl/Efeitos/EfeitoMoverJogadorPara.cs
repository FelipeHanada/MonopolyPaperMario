using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;

namespace MonopolyGame.Impl.Efeitos;


public class EfeitoMoverJogadorPara(int posicao) : IEfeitoJogador
{
    private readonly int posicao = posicao;


    public void Aplicar(Jogador jogador)
    {
        if (jogador == null)
        {
            throw new ArgumentNullException(nameof(jogador));
        }

        Console.WriteLine($"Efeito: Movendo {jogador.Nome} para casa 0.");

        // O método moveJogador no Tabuleiro lida com a lógica de:
        // 1. Movimento para frente (offset positivo).
        // 2. Movimento para trás (offset negativo).
        // 3. Verificação de ter passado pelo início (apenas se movendo para frente).
        jogador.Partida.Tabuleiro.MoverJogadorPara(jogador, posicao, true);
    }
}
