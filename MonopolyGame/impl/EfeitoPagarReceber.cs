using MonopolyPaperMario.Interface;
using MonopolyPaperMario.model;

namespace MonopolyPaperMario.Impl
{
    public class EfeitoPagarReceber : IEfeitoJogador
    {

        public void Execute(Joagador pagador, Joagador recebedor, int quantia)
        {
            // 1. Calcular o novo dinheiro do pagador
            int novoDinheiroPagador = pagador.getDinheiro() - quantia;
            pagador.setDinheiro(novoDinheiroPagador);

            // 2. Calcular o novo dinheiro do recebedor
            int novoDinheiroRecebedor = recebedor.getDinheiro() + quantia;
            recebedor.setDinheiro(novoDinheiroRecebedor);
        }

    }
}