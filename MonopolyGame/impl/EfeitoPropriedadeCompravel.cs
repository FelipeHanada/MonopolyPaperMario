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
                    // ==========================================================
                    // INÍCIO: CHECAGEM DO DUPLIGHOST
                    // ==========================================================
                    var duplighostEfeito = jogador.EfeitoDuplighostAtivo;

                    if (duplighostEfeito != null && duplighostEfeito.UsarEfeito(propriedade))
                    {
                        // O Duplighost irá pagar!
                        Jogador duplighost = duplighostEfeito.DuplighostAlvo;
                        
                        // 1. Calcular o aluguel base.
                        int aluguelBase = propriedade.CalcularPagamento(jogador);
                        
                        // 2. O pagamento do Duplighost pode ser afetado pelo desconto dele, não do jogador que caiu na casa.
                        // Usaremos o desconto DO DUPLIGHOST, se houver (o mais correto).
                        int aluguelFinal = duplighost.AplicarDesconto(aluguelBase); 
                        
                        // O jogador que ativou o Duplighost (que caiu na casa) não paga nada.
                        Console.WriteLine($"\n*** DUPLIGHOST ATIVO! ***");
                        Console.WriteLine($"{duplighost.Nome} (Duplighost) pagará o aluguel de ${aluguelFinal} (Base: ${aluguelBase}) para {propriedade.Proprietario.Nome}.");
                        
                        // Duplighost paga o aluguel ao Proprietário
                        duplighost.TransferirDinheiroPara(propriedade.Proprietario, aluguelFinal);
                        
                        // Limpa o efeito após o uso para evitar dupla utilização no mesmo turno
                        jogador.EfeitoDuplighostAtivo = null; 
                        
                        return; // Sai do método; a despesa foi resolvida.
                    }
                    // ==========================================================
                    // FIM: CHECAGEM DO DUPLIGHOST (Se não for acionado, segue o fluxo normal)
                    // ==========================================================

                    // FLUXO NORMAL DE PAGAMENTO (Muskular)
                    int aluguelBase = propriedade.CalcularPagamento(jogador);
                    
                    // Aplica o desconto do Muskular no valor do aluguel (do jogador que caiu na casa)
                    int aluguelFinal = jogador.AplicarDesconto(aluguelBase);

                    Console.WriteLine($"Esta propriedade pertence a {propriedade.Proprietario.Nome}. Você deve pagar ${aluguelFinal} de aluguel (Base: ${aluguelBase}).");
                    
                    // Usa o valor com desconto para a transferência
                    jogador.TransferirDinheiroPara(propriedade.Proprietario, aluguelFinal);
                }
                else if (propriedade.Proprietario == jogador)
                {
                    Console.WriteLine("Você parou em sua própria propriedade.");
                }
                else if (propriedade.Hipotecada)
                {
                    Console.WriteLine($"A propriedade {propriedade.Nome} está hipotecada. Nenhum aluguel é devido.");
                }
                return;
            }

            // ... (A lógica de compra abaixo não muda, pois Duplighost afeta apenas ALUGUEL)
            
            // Se a propriedade não tem dono, oferece para compra.
            int precoCompraBase = propriedade.Preco;
            int precoCompraFinal = jogador.AplicarDesconto(precoCompraBase);

            Console.WriteLine($"A propriedade {propriedade.Nome} está à venda por ${precoCompraFinal} (Preço base: ${precoCompraBase}).");
            Console.Write($"{jogador.Nome}, você deseja comprar? (s/n): ");
            string? resposta = Console.ReadLine()?.Trim().ToLower();

            if (resposta == "s")
            {
                // Verifica o saldo usando o preço com desconto
                if (jogador.Dinheiro >= precoCompraFinal)
                {
                    // Debita o preço com desconto
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