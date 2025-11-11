using MonopolyGame.Utils;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;

// A Reversão sempre zera o desconto, não importa qual era o valor anterior.

class EfeitoReverterDesconto : IEfeitoJogador
{
    public void Aplicar(Jogador jogador)
    {
        if (jogador == null) return;
        Log.WriteLine($"O efeito de desconto acabou. Descontos de {jogador.Nome} resetados.");
        jogador.Partida.AdicionarRegistro($"O efeito de desconto acabou. Descontos de {jogador.Nome} resetados.");
        
        // REVERSÃO: Define o desconto de volta para 0%
        jogador.Desconto = 0; 
    }
}
