using System;
using MonpolyMario.Components.Game.Model;

public class PropostaVenda
{
    private Jogador jogador;
    private PosseJogador posseRequisitada;
    private int preco;

    public PropostaVenda(int valor, PosseJogador posseRequisitada, Jogador jogador)
    {
        this.preco = valor;
        this.posseRequisitada = posseRequisitada;
        this.jogador = jogador;
    }

    public bool SerAceito()
    {
        string nome = jogador.getNome();
        Console.Write($"{nome} aceita a proposta? (S/N)");

        string resposta = Console.ReadLine();

        if (resposta.ToLower() == "s" || resposta.ToLower() == "sim")
        {
            Console.Write("Proposta aceita!");
            return true;
        }
        else
        {
            Console.Write("Proposta Recusada!");
            return false;
        }
    }
}