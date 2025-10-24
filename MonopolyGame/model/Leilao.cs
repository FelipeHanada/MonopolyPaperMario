using System.Collections.Generic;
using System;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class Leilao
    {
        public Propriedade Propriedade { get; private set; }
        public List<Jogador> Participantes { get; private set; }
        public Jogador jogador { get; private set; }
        public int LanceAtual { get; private set; }
        public Jogador? MaiorLicitante { get; private set; } // Correção aqui

        public Leilao(Propriedade propriedade, List<Jogador> participantes, Jogador jogador)
        {
            Propriedade = propriedade;
            Participantes = participantes;
            this.jogador = jogador;
            LanceAtual = 0;
            MaiorLicitante = null; // Inicialização explícita
        }

        public void Executar()
        {
            Console.Write("Leilão iniciado");
            int valorFinal = 0;
            int index = (Participantes.IndexOf(jogador) + 1) % Participantes.Count;

            Jogador jogadorAtual = Participantes[index];

            while (Participantes.Count > 1)
            {

                Console.Write($"\n{jogadorAtual.Nome} gostaria dar um lance? (10|50|100|sair)");
                string? resposta = Console.ReadLine();

                switch (resposta)
                {
                    case "10":
                        valorFinal += 10;
                        break;

                    case "50":
                        valorFinal += 50;
                        break;

                    case "100":
                        valorFinal += 100;
                        break;

                    case "sair":
                        Console.Write($"{jogadorAtual.Nome} está fora.");
                        int indexDelete = index;
                        Participantes.RemoveAt(indexDelete);
                        if (index == 0)
                        {
                            index = Participantes.Count - 1;
                        }
                        else
                        {
                            index -= 1;
                        }
                        break;
                }

                index = (index + 1) % Participantes.Count;
                jogadorAtual = Participantes[index];
            }

            MaiorLicitante = Participantes[0];

            Console.Write($"{MaiorLicitante!.Nome} venceu por {valorFinal}");

            MaiorLicitante!.Debitar(valorFinal);
            MaiorLicitante!.AdicionarPropriedade(Propriedade);
        }
    }
}