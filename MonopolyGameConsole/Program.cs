using MonopolyGame.Model.Partidas;

namespace MonopolyGameConsole;

class Program
{
    private static readonly Partida partida = new(["Mario", "Luigi", "Peach", "Yoshi"]);

    static void Main(string[] args)
    {
        Console.WriteLine("🎲 Monopoly Paper Mario! 🎲");

        Console.WriteLine("Jogadores:");
        foreach (Jogador jogador in partida.Jogadores)
        {
            Console.WriteLine("- " + jogador.Nome);
        }
        Console.WriteLine();

        // Loop principal do jogo
        while (partida.JogadoresAtivos.Count() > 1)
        {
            partida.ProximoTurno(); // Apenas avança o ponteiro do jogador atual

            TurnoJogador.Instance.IniciarTurno(partida);
        }

        var vencedor = partida.Jogadores.FirstOrDefault(j => !j.Falido);
        if (vencedor != null)
        {
            Console.WriteLine($"\n🎉 Fim de jogo! O vencedor é {vencedor.Nome}! 🎉");
        }
    }

    //static void IniciarTurno()
    //{
    //    Console.WriteLine()
    //}
}
