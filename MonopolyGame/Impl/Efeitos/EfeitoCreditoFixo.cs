using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;


// Efeito genérico para creditar um valor fixo (prêmio).
public class EfeitoCreditoFixo(Partida partida, int valor) : EfeitoJogador(partida)
{
    private readonly int valor = valor;

    public override void Aplicar(Jogador jogador)
    {
        if (jogador == null) return;
        
        // O jogador recebe o valor.
        // O Creditar() já deve lidar com a atualização do saldo.
        jogador.Creditar(valor);
    }
}
