using System;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class Imovel : Propriedade
    {
        public string Cor { get; private set; }
        public int[] Alugueis { get; private set; } // 6 posições: terreno, 1-4 casas, hotel
        public int NivelConstrucao { get; private set; } // 0 = terreno, 5 = hotel
        public int CustoCasa { get; private set; }

        public Imovel(string nome, int preco, string cor, int[] alugueis, int custoCasa)
            : base(nome, preco)
        {
            if (alugueis.Length != 6)
                throw new ArgumentException("O vetor de aluguéis deve ter 6 posições.");

            this.Cor = cor;
            this.Alugueis = alugueis;
            this.CustoCasa = custoCasa;
            this.NivelConstrucao = 0;
        }

        public override int CalcularPagamento(Jogador jogador)
        {
            if (Proprietario == null || Hipotecada) return 0;

            // Se não há construções, verifica se há monopólio para dobrar o pagamento.
            if (NivelConstrucao == 0 && Monopolio.VerificarMonopolio(Proprietario, this.Cor))
            {
                return Alugueis[0] * 2;
            }
            
            return Alugueis[NivelConstrucao];
        }
    }
}