using MonopolyGame.Utils;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;

namespace MonopolyGame.Impl.Efeitos;


/// Efeito imediato ativado pelo Magikoopa Vermelho.
/// Define um multiplicador de 1.5 para a próxima jogada de dados do jogador.

public class EfeitoMagikoopaVermelho : IEfeitoJogador
{
    public void Aplicar(Jogador jogador)
    {
        Log.WriteLine($"--- Efeito Magikoopa Vermelho ativado para {jogador.Nome} ---");
        if (jogador.Multiplicador == 0)
        {
            jogador.Multiplicador = 2;
        }
        else
        {
            jogador.Multiplicador = 0;
        }
        Log.WriteLine("=================DEBUG===============\nMultiplicador: " + jogador.Multiplicador);
    }
}
