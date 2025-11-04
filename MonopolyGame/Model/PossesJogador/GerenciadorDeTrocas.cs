using MonopolyGame.Interface;
using MonopolyGame.Interface.PropostasTroca;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PropostasTroca;

namespace MonopolyGame.Model.PossesJogador;


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
        var possesDesejadas = new List<IPosseJogador>();
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
            possesDesejadas.ForEach(p => Console.WriteLine($"- {p.Nome}"));
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
                    if (possesDesejadas.Contains(posseSelecionada)) possesDesejadas.Remove(posseSelecionada);
                    else possesDesejadas.Add(posseSelecionada);
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
        var proposta = new PropostaTroca(ofertante, jogadorAlvo);
        foreach (IPosseJogador posseOfertada in possesOfertante)
        {
            proposta.PossesOfertadas.Add(posseOfertada);
        }
        foreach (IPosseJogador posseDesejada in possesDesejadas)
        {
            proposta.PossesDesejadas.Add(posseDesejada);
        }
        proposta.DinheiroOfertado = dinheiro;

        Console.Clear();
        Console.WriteLine($"\n--- Proposta para {jogadorAlvo.Nome} ---");
        Console.WriteLine($"{ofertante.Nome} oferece:");
        proposta.PossesOfertadas.ForEach(p => Console.WriteLine($"- {p.Nome}"));
        if (proposta.DinheiroOfertado > 0) Console.WriteLine($"- ${proposta.DinheiroOfertado}");

        Console.WriteLine("\nEm troca de:");
        proposta.PossesDesejadas.ForEach(p => Console.WriteLine($"- {p.Nome}"));
        if (proposta.DinheiroOfertado < 0) Console.WriteLine($"- ${-proposta.DinheiroOfertado}");

        Console.Write($"\n{jogadorAlvo.Nome}, você aceita? (s/n): ");
        if (Console.ReadLine()?.ToLower() == "s")
        {
            proposta.Efetuar();
            //ExecutarTroca(proposta);
        }
        else
        {
            Console.WriteLine("Proposta recusada.");
        }
    }

    //private void ExecutarTroca(IPropostaTroca proposta)
    //{
    //    try
    //    {
    //        // Validar dinheiro antes de qualquer transferência
    //        if (proposta.DinheiroOfertado > 0 && proposta.Ofertante.Dinheiro < proposta.DinheiroOfertado)
    //        {
    //            throw new InvalidOperationException($"{proposta.Ofertante.Nome} não tem dinheiro suficiente para a troca.");
    //        }
    //        if (proposta.DinheiroOfertado < 0 && proposta.Destinatario.Dinheiro < -proposta.DinheiroOfertado)
    //        {
    //            throw new InvalidOperationException($"{proposta.Destinatario.Nome} não tem dinheiro suficiente para a troca.");
    //        }

    //        // Transferir dinheiro
    //        if (proposta.DinheiroOfertado != 0)
    //        {
    //            proposta.Ofertante.TransferirDinheiroPara(proposta.Destinatario, proposta.DinheiroOfertado);
    //        }

    //        // Transferir posses do ofertante para o alvo
    //        foreach (var posse in proposta.PossesOfertadas)
    //        {
    //            proposta.Ofertante.TransferirPossePara(proposta.Destinatario, posse);
    //        }
    //        // Transferir posses do alvo para o ofertante
    //        foreach (var posse in proposta.PossesDesejadas)
    //        {
    //            proposta.Destinatario.TransferirPossePara(proposta.Ofertante, posse);
    //        }

    //        Console.WriteLine("Troca realizada com sucesso!");
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"A troca falhou: {ex.Message}");
    //    }
    //}
}