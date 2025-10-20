using MonpolyMario.Components.Game.Model;
using System;
using System.Collections.Generic;

public class Leilao
{
    private UltimoLance ultimoLance;
    private PosseJogador propriedade;
    private List<Jogador> jogadores;

    public Leilao(PosseJogador propriedade, Jogador jogadorInicial)
    {
        this.propriedade = propriedade;
        this.ultimoLance = new UltimoLance(jogadorInicial, 0);
        this.jogadores = new List<Jogador>(Partida.getPartida().getJogadores());
    }

    public void ExecutarLeilao()
    {
        Console.WriteLine("Iniciando leilão!");

        int indiceJogador = 0;

        while (jogadores.Count > 1)
        {
            Jogador jogadorAtual = jogadores[indiceJogador];
            int valorAtual = ultimoLance.getUltimolance();

            Console.WriteLine($"\nValor atual: {valorAtual}");
            Console.WriteLine($"{jogadorAtual.getNome()}, quer aumentar a aposta? (10|50|100|sair)");
            string resposta = Console.ReadLine()?.Trim();

            switch (resposta)
            {
                case "10":
                case "50":
                case "100":
                    int incremento = int.Parse(resposta);
                    ultimoLance.setUltimoLance(valorAtual + incremento);
                    ultimoLance.setJogador(jogadorAtual);
                    Console.WriteLine($"{jogadorAtual.getNome()} aumentou o lance para {valorAtual + incremento}!");
                    break;

                case "sair":
                    Console.WriteLine($"{jogadorAtual.getNome()} saiu do leilão.");
                    jogadores.RemoveAt(indiceJogador);
                    if (indiceJogador >= jogadores.Count)
                        indiceJogador = 0;
                    continue;

                // Não vou tratar um caso onde "resposta" possui um valor inválido,
                // pois essa parte será reescritao na implementação da interface.
            }

            indiceJogador = (indiceJogador + 1) % jogadores.Count;
        }

        Jogador vencedor = jogadores[0];
        int valorFinal = ultimoLance.getUltimolance();

        vencedor.setDinheiro(vencedor.getDinheiro() - valorFinal);
        vencedor.addPosse(propriedade);

        Console.WriteLine($"\nLeilão encerrado! {vencedor.getNome()} venceu com o lance de {valorFinal}.");

    }
}