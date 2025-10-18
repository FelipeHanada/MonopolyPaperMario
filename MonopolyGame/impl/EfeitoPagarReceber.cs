using MonopolyPaperMario.Model; 
using MonopolyPaperMario.Interface;

namespace MonopolyPaperMario.Impl
{
    public class EfeitoPagarReceber : IEfeitoJogador
    {

        public void Execute(Player pagador, Player recebedor, double quantia)
        {
            // 1. Calcular o novo dinheiro do pagador
            double novoDinheiroPagador = pagador.getDinheiro() - quantia;
            pagador.setDinheiro(novoDinheiroPagador);

            // 2. Calcular o novo dinheiro do recebedor
            double novoDinheiroRecebedor = recebedor.getDinheiro() + quantia;
            recebedor.setDinheiro(novoDinheiroRecebedor);
        }

    }
}