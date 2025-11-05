using MonopolyGame.Utils;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;


public class EfeitoRotacionarPosicao : IEfeitoJogador
{
    public void Aplicar(Jogador jogador)
    {
        // O jogador 'jogador' é o que pegou a carta, mas o efeito é GLOBAL.
        Log.WriteLine($"Efeito Sentinel Ativado! Jogador {jogador.Nome} iniciou a rotação de todas as posições.");
        
        jogador.Partida.Tabuleiro.RotacionarPosicoesJogadores();
    }
}
