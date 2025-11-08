using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PossesJogador;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.PropostasTroca;
using MonopolyGame.Utils;

namespace MonopolyGame.Impl.Efeitos;


public class EfeitoPropriedadeCompravel(Propriedade propriedade) : IEfeitoJogador
{
    private readonly Propriedade propriedade = propriedade;

    public void Aplicar(Jogador jogador)
    {
        if (jogador == null) throw new ArgumentNullException(nameof(jogador));

        if (propriedade.Proprietario == null)
        {
            PropostaTroca propostaTroca = new(null, jogador);
            propostaTroca.PossesOfertadas.Add(propriedade);
            propostaTroca.DinheiroOfertado = -propriedade.Preco;

            Log.WriteLine("Jogador " + jogador.Nome + " caiu em uma casa comprável sem dono, iniciando fase de proposta troca.");
            jogador.Partida.IniciarPropostaTroca(propostaTroca);
        } else
        {
            // paga aluguel
        }


        //    // Se a propriedade tem dono, calcula e cobra o aluguel.
        //    if (propriedade.Proprietario != null)
        //    {
        //        if (propriedade.Proprietario != jogador && !propriedade.Hipotecada)
        //        {
        //            // Essas duas tavam declaradas 2x dentro de blocos diferentes, dando erro de escopo
        //            int aluguelBase;
        //            int aluguelFinal;

        //            var duplighostEfeito = jogador.EfeitoDuplighostAtivo;

        //            if (duplighostEfeito != null && duplighostEfeito.UsarEfeito(propriedade))
        //            {
        //                Jogador duplighost = duplighostEfeito.DuplighostAlvo;

        //                aluguelBase = propriedade.CalcularPagamento(jogador);
        //                aluguelFinal = duplighost.AplicarDesconto(aluguelBase);

        //                Log.WriteLine($"\n*** DUPLIGHOST ATIVO! ***");
        //                Log.WriteLine($"{duplighost.Nome} (Duplighost) pagará o aluguel de ${aluguelFinal} (Base: ${aluguelBase}) para {propriedade.Proprietario.Nome}.");

        //                duplighost.TransferirDinheiroPara(propriedade.Proprietario, aluguelFinal);

        //                jogador.EfeitoDuplighostAtivo = null;

        //                return;
        //            }

        //            // FLUXO NORMAL DE PAGAMENTO (Muskular)
        //            aluguelBase = propriedade.CalcularPagamento(jogador);
        //            aluguelFinal = jogador.AplicarDesconto(aluguelBase);

        //            Log.WriteLine($"Esta propriedade pertence a {propriedade.Proprietario.Nome}. Você deve pagar ${aluguelFinal} de aluguel (Base: ${aluguelBase}).");

        //            jogador.TransferirDinheiroPara(propriedade.Proprietario, aluguelFinal);
        //        }
        //        else if (propriedade.Proprietario == jogador && propriedade.Hipotecada == false)
        //        {
        //            Log.WriteLine("Você parou em sua própria propriedade.");
        //        }
        //        else if (propriedade.Hipotecada)
        //        {
        //            Log.WriteLine($"A propriedade {propriedade.Nome} está hipotecada. Nenhum aluguel é devido.");
        //        }
        //        return;
        //    }

        //    // Se a propriedade não tem dono, oferece para compra.
        //    int precoCompraBase = propriedade.Preco;
        //    int precoCompraFinal = jogador.AplicarDesconto(precoCompraBase);

        //    Log.WriteLine($"A propriedade {propriedade.Nome} está à venda por ${precoCompraFinal} (Preço base: ${precoCompraBase}).");
        //    Console.Write($"{jogador.Nome}, você deseja comprar? (s/n): ");
        //    string? resposta = Console.ReadLine()?.Trim().ToLower();

        //    if (resposta == "s")
        //    {
        //        if (jogador.Dinheiro >= precoCompraFinal)
        //        {
        //            jogador.Debitar(precoCompraFinal); 
        //            propriedade.Proprietario = jogador;
        //            jogador.AdicionarPosse(propriedade);
        //            Log.WriteLine($"Parabéns! {jogador.Nome} comprou {propriedade.Nome}.");
        //        }
        //        else
        //        {
        //            Log.WriteLine("Você não tem dinheiro suficiente. A propriedade irá a leilão.");
        //            TurnoJogador.Instance.IniciarLeilao(propriedade);
        //        }
        //    }
        //    else
        //    {
        //        Log.WriteLine($"{jogador.Nome} decidiu não comprar. A propriedade irá a leilão.");
        //        TurnoJogador.Instance.IniciarLeilao(propriedade);
        //    }
    }
}
