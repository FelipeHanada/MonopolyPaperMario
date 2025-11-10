using MonopolyGame.Model.Partidas;
using MonopolyGame.Utils;

namespace MonopolyGame.Model.PossesJogador;


public class Imovel(string nome, int preco, int hipoteca, PropriedadeCor cor, int[] alugueis, int precoComprarCasa, int precoVenderCasa) : Propriedade(nome, preco, hipoteca, cor)
{
    public int[] Alugueis { get; private set; } = alugueis; // 6 posições: terreno, 1-4 casas, hotel
    public int NivelConstrucao { get; private set; } = 0; // 0 = terreno, 5 = hotel
    public int PrecoComprarCasa { get; private set; } = precoComprarCasa;
    public int PrecoVenderCasa { get; private set; } = precoVenderCasa;

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

    public override bool PodeHipotecar()
    {
        if (Proprietario == null)
        {
            Log.WriteLine(" Este imóvel não tem proprietário.");
            return false;
        }

        var imoveisMesmaCor = Proprietario.Posses
            .OfType<Imovel>()
            .Where(imovel => imovel.Cor == Cor)
            .ToList();

        int maiorNivel = imoveisMesmaCor.Max(imovel => imovel.NivelConstrucao);

        return base.PodeHipotecar() && maiorNivel == 0;
    }

    public bool PodeAdicionarCasa()
    {
        if (Proprietario == null)
        {
            Log.WriteLine(" Este imóvel não tem proprietário.");
            return false;
        }

        bool monopolio = Monopolio.VerificarMonopolio(Proprietario, Cor);
        if (!monopolio)
        {
            Log.WriteLine("É necessário possuir todos os imóveis desta cor para evoluir!!");
            //Log.WriteLine("É necessário possuir todos os imóveis desta cor para evoluir!!");
            return false;
        }

        if (NivelConstrucao == 5)
        {
            Log.WriteLine("Imóvel em nível máximo!!!");
            //Log.WriteLine("Imóvel em nível máximo!!!");
            return false;
        }

        var imoveisMesmaCor = Proprietario.Posses
            .OfType<Imovel>()
            .Where(imovel => imovel.Cor == Cor)
            .ToList();

        if (imoveisMesmaCor.Count(imovel => imovel.Hipotecada) > 0) return false;

        int menorNivel = imoveisMesmaCor.Min(imovel => imovel.NivelConstrucao);

        if (NivelConstrucao > menorNivel)
        {
            Log.WriteLine("Você só pode construir na propriedade com menor número de casas do conjunto!");
            //Log.WriteLine("Você só pode construir na propriedade com menor número de casas do conjunto!");
            return false;
        }

        return true;
    }

    public bool AdicionarCasa()
    {
        if (!PodeAdicionarCasa()) return false;

        NivelConstrucao++;
        Log.WriteLine("Propriedade adicionada com sucesso!!");
        //Log.WriteLine("Propriedade adicionada com sucesso!!");
        return true;
    }

    public bool PodeRemoverCasa()
    {
        if (Proprietario == null)
        {
            Log.WriteLine("Este imóvel não tem proprietário.");
            return false;
        }
        if (NivelConstrucao == 0)
        {
            return false;
        }

        var imoveisMesmaCor = Proprietario.Posses
            .OfType<Imovel>()
            .Where(imovel => imovel.Cor == Cor)
            .ToList();

        int maiorNivel = imoveisMesmaCor.Max(imovel => imovel.NivelConstrucao);

        if (NivelConstrucao < maiorNivel)
        {
            return false;
        }

        return true;
    }

    public bool RemoverCasa()
    {
        if (!PodeRemoverCasa()) return false;
        NivelConstrucao--;
        return true;
    }
}
