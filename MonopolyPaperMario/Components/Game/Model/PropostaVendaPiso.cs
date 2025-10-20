using System;
using MonpolyMario.Components.Game.Model;

public class PropostaVendaPiso : PropostaVenda
{
    private Leilao? leilao;
    private Jogador jogador;
    private PosseJogador posseRequisitada;
    private int preco;

    public PropostaVendaPiso(int preco, PosseJogador posseRequisitada, Jogador jogador)
     : base(preco, posseRequisitada, jogador)
    {
        this.preco = preco;
        this.posseRequisitada = posseRequisitada;
        this.jogador = jogador;
    }

    public new bool SerAceito()
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

            leilao = new Leilao(posseRequisitada, jogador);
            leilao.ExecutarLeilao();

            return false;
        }
    }
}