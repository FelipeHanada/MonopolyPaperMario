using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Exceptions;

namespace MonopolyGame.Impl.Efeitos;


public class EfeitoDebitoFixo(Partida partida, int valor) : EfeitoJogador(partida)
{
    private readonly int valor = valor;
    
    public override void Aplicar(Jogador jogador)
    {
        if (jogador == null) return;
        
        // Usamos a Descrição da Carta para o console.log (menos dependência do efeito)
        Console.WriteLine($"{jogador.Nome} deve pagar ${valor}.");
        
        try
        {
            // Debita o valor com desconto
            jogador.Debitar(valor);
            Console.WriteLine($"Transação concluída. Novo saldo de {jogador.Nome}: ${jogador.Dinheiro}");
        }
        catch (FundosInsuficientesException)
        {
            // Lógica de falência/hipoteca
            Console.WriteLine($"{jogador.Nome} não conseguiu pagar a multa de ${valor} e deve tomar medidas (hipotecar ou falir).");
            jogador.SetFalido(true);
        }
    }
}
