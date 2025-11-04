using System;
using System.Collections.Generic;
using System.Linq;
using MonopolyGame.Interface;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PropostasVenda;

namespace MonopolyGame.Model.PossesJogador
{
    public class GerenciadorDeTrocas
    {
        private readonly List<Jogador> jogadores;

        public GerenciadorDeTrocas(List<Jogador> jogadores)
        {
            this.jogadores = jogadores;
        }

        public void IniciarNovaTroca(Jogador ofertante)
        {
            if (ofertante == null) return;

            // 1. Escolher com quem trocar
            var outrosJogadores = jogadores.Where(j => j != ofertante && !j.Falido).ToList();
            if (!outrosJogadores.Any())
            {
                Console.WriteLine("Não há outros jogadores para fazer uma troca.");
                return;
            }

            Console.WriteLine("\nCom qual jogador você gostaria de trocar?");
            for (int i = 0; i < outrosJogadores.Count; i++)
            {
                Console.WriteLine($"{i} - {outrosJogadores[i].Nome}");
            }
            Console.Write("Escolha: ");
            if (!int.TryParse(Console.ReadLine(), out int indiceAlvo) || indiceAlvo < 0 || indiceAlvo >= outrosJogadores.Count)
            {
                Console.WriteLine("Seleção inválida.");
                return;
            }
            Jogador jogadorAlvo = outrosJogadores[indiceAlvo];

            // 2. Montar a proposta
            var possesOfertante = new List<IPosseJogador>();
            var possesAlvo = new List<IPosseJogador>();
            int dinheiro = 0;

            var todasAsPosses = ofertante.Posses.Cast<IPosseJogador>().ToList();
            todasAsPosses.AddRange(jogadorAlvo.Posses.Cast<IPosseJogador>());

            bool montando = true;
            while (montando)
            {
                Console.Clear();
                Console.WriteLine("--- Montando Proposta de Troca ---");
                Console.WriteLine($"Ofertante: {ofertante.Nome} | Alvo: {jogadorAlvo.Nome}\n");

                Console.WriteLine("Sua oferta:");
                possesOfertante.ForEach(p => Console.WriteLine($"- {p.Nome}"));
                Console.WriteLine("\nItens pedidos:");
                possesAlvo.ForEach(p => Console.WriteLine($"- {p.Nome}"));
                Console.WriteLine($"\nDinheiro: {(dinheiro > 0 ? $"Você oferece ${dinheiro}" : dinheiro < 0 ? $"Você pede ${-dinheiro}" : "$0")}");

                Console.WriteLine("\nOpções:");
                for (int i = 0; i < todasAsPosses.Count; i++)
                {
                    var posse = todasAsPosses[i];
                    string dono = posse.Proprietario == ofertante ? "(Seu)" : "(Dele)";
                    Console.WriteLine($"{i} - Adicionar/Remover {posse.Nome} {dono}");
                }
                Console.WriteLine("d - Definir valor em dinheiro");
                Console.WriteLine("f - Finalizar e fazer proposta");
                Console.WriteLine("c - Cancelar proposta");
                Console.Write("Escolha: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int indexPosse) && indexPosse >= 0 && indexPosse < todasAsPosses.Count)
                {
                    var posseSelecionada = todasAsPosses[indexPosse];
                    if (posseSelecionada.Proprietario == ofertante)
                    {
                        if (possesOfertante.Contains(posseSelecionada)) possesOfertante.Remove(posseSelecionada);
                        else possesOfertante.Add(posseSelecionada);
                    }
                    else
                    {
                        if (possesAlvo.Contains(posseSelecionada)) possesAlvo.Remove(posseSelecionada);
                        else possesAlvo.Add(posseSelecionada);
                    }
                }
                else if (input == "d")
                {
                    Console.Write("Digite o valor (positivo para oferecer, negativo para pedir): ");
                    int.TryParse(Console.ReadLine(), out dinheiro);
                }
                else if (input == "f") montando = false;
                else if (input == "c") return;
            }

            // 3. Apresentar e processar a proposta
            var proposta = new PropostaDeTroca(ofertante, jogadorAlvo, possesOfertante, possesAlvo, dinheiro);
            Console.Clear();
            Console.WriteLine($"\n--- Proposta para {jogadorAlvo.Nome} ---");
            Console.WriteLine($"{ofertante.Nome} oferece:");
            proposta.PossesDoOfertante.ForEach(p => Console.WriteLine($"- {p.Nome}"));
            if (proposta.ValorEmDinheiro > 0) Console.WriteLine($"- ${proposta.ValorEmDinheiro}");

            Console.WriteLine("\nEm troca de:");
            proposta.PossesDoAlvo.ForEach(p => Console.WriteLine($"- {p.Nome}"));
            if (proposta.ValorEmDinheiro < 0) Console.WriteLine($"- ${-proposta.ValorEmDinheiro}");

            Console.Write($"\n{jogadorAlvo.Nome}, você aceita? (s/n): ");
            if (Console.ReadLine()?.ToLower() == "s")
            {
                ExecutarTroca(proposta);
            }
            else
            {
                Console.WriteLine("Proposta recusada.");
            }
        }

        private void ExecutarTroca(PropostaDeTroca proposta)
        {
            try
            {
                // Validar dinheiro antes de qualquer transferência
                if (proposta.ValorEmDinheiro > 0 && proposta.Ofertante.Dinheiro < proposta.ValorEmDinheiro)
                {
                    throw new InvalidOperationException($"{proposta.Ofertante.Nome} não tem dinheiro suficiente para a troca.");
                }
                if (proposta.ValorEmDinheiro < 0 && proposta.Alvo.Dinheiro < -proposta.ValorEmDinheiro)
                {
                    throw new InvalidOperationException($"{proposta.Alvo.Nome} não tem dinheiro suficiente para a troca.");
                }

                // Transferir dinheiro
                if (proposta.ValorEmDinheiro != 0)
                {
                    proposta.Ofertante.TransferirDinheiroPara(proposta.Alvo, proposta.ValorEmDinheiro);
                }

                // Transferir posses do ofertante para o alvo
                foreach (var posse in proposta.PossesDoOfertante)
                {
                    proposta.Ofertante.TransferirPossePara(proposta.Alvo, posse);
                }
                // Transferir posses do alvo para o ofertante
                foreach (var posse in proposta.PossesDoAlvo)
                {
                    proposta.Alvo.TransferirPossePara(proposta.Ofertante, posse);
                }

                Console.WriteLine("Troca realizada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A troca falhou: {ex.Message}");
            }
        }
    }
}