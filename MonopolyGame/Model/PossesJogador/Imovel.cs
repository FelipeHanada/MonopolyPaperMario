using MonopolyGame.Model.Partidas;
using System;
using System.Linq;

namespace MonopolyGame.Model.PossesJogador
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

            Cor = cor;
            Alugueis = alugueis;
            CustoCasa = custoCasa;
            NivelConstrucao = 0;
        }

        public override int CalcularPagamento(Jogador jogador)
        {
            if (Proprietario == null || Hipotecada) return 0;

            // Se não há construções, verifica se há monopólio para dobrar o pagamento.
            if (NivelConstrucao == 0 && Monopolio.VerificarMonopolio(Proprietario, Cor))
            {
                return Alugueis[0] * 2;
            }

            return Alugueis[NivelConstrucao];
        }

        public void AdicionarCasa()
        {
            bool monopolio = Monopolio.VerificarMonopolio(Proprietario, Cor);

            if (!monopolio)
            {
                Console.WriteLine("É necessário possuir todos os imóveis desta cor para evoluir!!");
                return;
            }

            if (NivelConstrucao == 5)
            {
                Console.WriteLine("Imóvel em nível máximo!!!");
                return;
            }

            var imoveisMesmaCor = Proprietario.Posses
                .OfType<Imovel>()
                .Where(imovel => imovel.Cor == Cor)
                .ToList();

            int menorNivel = imoveisMesmaCor.Min(imovel => imovel.NivelConstrucao);

            if (NivelConstrucao > menorNivel)
            {
                Console.WriteLine("Você só pode construir na propriedade com menor número de casas do conjunto!");
                return;
            }
            else
            {
                Proprietario.Debitar(CustoCasa);
                NivelConstrucao++;
                Console.WriteLine("Propriedade adicionada com sucesso!!");
            }
        }
    }
}