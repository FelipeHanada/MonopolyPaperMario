using System.Collections.Generic;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class Leilao
    {
        public Propriedade Propriedade { get; private set; }
        public List<Jogador> Participantes { get; private set; }
        public int LanceAtual { get; private set; }
        public Jogador? MaiorLicitante { get; private set; } // Correção aqui

        public Leilao(Propriedade propriedade, List<Jogador> participantes)
        {
            Propriedade = propriedade;
            Participantes = participantes;
            LanceAtual = 0;
            MaiorLicitante = null; // Inicialização explícita
        }

        public void DarLance(Jogador jogador, int valor)
        {
            if (valor > LanceAtual)
            {
                LanceAtual = valor;
                MaiorLicitante = jogador;
                System.Console.WriteLine($"Novo lance de ${valor} por {jogador.Nome}.");
            }
        }
    }
}