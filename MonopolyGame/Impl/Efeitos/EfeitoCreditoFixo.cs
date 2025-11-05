using MonopolyGame.Utils;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;


// Efeito genérico para creditar um valor fixo (prêmio).
public class EfeitoCreditoFixo(int valor) : IEfeitoJogador
{
    private readonly int valor = valor;

    public void Aplicar(Jogador jogador)
    {
        if (jogador == null) return;

        // O jogador recebe o valor.
        // O Creditar() já deve lidar com a atualização do saldo.
        Log.WriteLine("O jogador " + jogador.Nome + " ganhou " + valor + " moedas!");
        jogador.Creditar(valor);
    }
}
