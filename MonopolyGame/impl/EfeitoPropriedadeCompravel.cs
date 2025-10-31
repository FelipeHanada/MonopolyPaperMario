using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;
using MonopolyPaperMario.MonopolyGame.Impl; 

namespace MonopolyPaperMario.MonopolyGame.Impl
{
    public class EfeitoPropriedadeCompravel : IEfeitoJogador
    {
        private readonly Propriedade propriedade;
        private readonly Partida partida;

        public EfeitoPropriedadeCompravel(Propriedade propriedade, Partida partida)
        {
            this.propriedade = propriedade ?? throw new ArgumentNullException(nameof(propriedade));
            this.partida = partida ?? throw new ArgumentNullException(nameof(partida));
        }

        public void Execute(Jogador jogador)
        {
            if (jogador == null) throw new ArgumentNullException(nameof(jogador));

            // Se a propriedade tem dono, calcula e cobra o aluguel.
            if (propriedade.Proprietario != null)
            {
                if (propriedade.Proprietario != jogador && !propriedade.Hipotecada)
                {
                    // Essas duas tavam declaradas 2x dentro de blocos diferentes, dando erro de escopo
                    int aluguelBase;
                    int aluguelFinal;

                    var duplighostEfeito = jogador.EfeitoDuplighostAtivo;

                    if (duplighostEfeito != null && duplighostEfeito.UsarEfeito(propriedade))
                    {
                        Jogador duplighost = duplighostEfeito.DuplighostAlvo;
                        
                        aluguelBase = propriedade.CalcularPagamento(jogador);
                        aluguelFinal = duplighost.AplicarDesconto(aluguelBase); 
                        
                        Console.WriteLine($"\n*** DUPLIGHOST ATIVO! ***");
                        Console.WriteLine($"{duplighost.Nome} (Duplighost) pagará o aluguel de ${aluguelFinal} (Base: ${aluguelBase}) para {propriedade.Proprietario.Nome}.");
                        
                        duplighost.TransferirDinheiroPara(propriedade.Proprietario, aluguelFinal);
                        
                        jogador.EfeitoDuplighostAtivo = null; 
                        
                        return;
                    }

                    // FLUXO NORMAL DE PAGAMENTO (Muskular)
                    aluguelBase = propriedade.CalcularPagamento(jogador);
                    aluguelFinal = jogador.AplicarDesconto(aluguelBase);

                    Console.WriteLine($"Esta propriedade pertence a {propriedade.Proprietario.Nome}. Você deve pagar ${aluguelFinal} de aluguel (Base: ${aluguelBase}).");
                    
                    jogador.TransferirDinheiroPara(propriedade.Proprietario, aluguelFinal);
                }
                else if (propriedade.Proprietario == jogador && propriedade.Hipotecada == false)
                {
                    Console.WriteLine("Você parou em sua própria propriedade.");
                }
                else if (propriedade.Hipotecada)
                {
                    Console.WriteLine($"A propriedade {propriedade.Nome} está hipotecada. Nenhum aluguel é devido.");
                }
                return;
            }
            
            // Se a propriedade não tem dono, oferece para compra.
            int precoCompraBase = propriedade.Preco;
            int precoCompraFinal = jogador.AplicarDesconto(precoCompraBase);

            Console.WriteLine($"A propriedade {propriedade.Nome} está à venda por ${precoCompraFinal} (Preço base: ${precoCompraBase}).");
            Console.Write($"{jogador.Nome}, você deseja comprar? (s/n): ");
            string? resposta = Console.ReadLine()?.Trim().ToLower();

            if (resposta == "s")
            {
                if (jogador.Dinheiro >= precoCompraFinal)
                {
                    jogador.Debitar(precoCompraFinal); 
                    propriedade.Proprietario = jogador;
                    jogador.AdicionarPropriedade(propriedade);
                    Console.WriteLine($"Parabéns! {jogador.Nome} comprou {propriedade.Nome}.");
                }
                else
                {
                    Console.WriteLine("Você não tem dinheiro suficiente. A propriedade irá a leilão.");
                    TurnoJogador.Instance.IniciarLeilao(propriedade);
                }
            }
            else
            { 
                Console.WriteLine($"{jogador.Nome} decidiu não comprar. A propriedade irá a leilão.");
                TurnoJogador.Instance.IniciarLeilao(propriedade);
            }
        }
    }
}