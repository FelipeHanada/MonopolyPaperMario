using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;

namespace MonopolyGame.Impl.Efeitos;


// Responsável por mover o jogador um número (positivo ou negativo) de casas.
public class EfeitoMoverJogador(int offset) : IEfeitoJogador
{
    // CAMPOS NECESSÁRIOS:
    private readonly int offset = offset;

    // EXECUÇÃO: Usa o Tabuleiro para aplicar o movimento.
    public void Aplicar(Jogador jogador)
    {
        Console.WriteLine($"Efeito: Movendo {jogador.Nome} por {offset} casas.");

        // O método moveJogador no Tabuleiro lida com a lógica de:
        // 1. Movimento para frente (offset positivo).
        // 2. Movimento para trás (offset negativo).
        // 3. Verificação de ter passado pelo início (apenas se movendo para frente).
        jogador.Partida.Tabuleiro.MoveJogador(jogador, offset);
    }
}
