using MonopolyGame.Utils;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Exceptions;

namespace MonopolyGame.Impl.Efeitos;


public class EfeitoDebitoFixo(int valor) : IEfeitoJogador
{
    private readonly int valor = valor;
    
    public void Aplicar(Jogador jogador)
    {
        if (jogador == null) return;

        // Usamos a Descrição da Carta para o console.log (menos dependência do efeito)
        Log.WriteLine($"{jogador.Nome} deve pagar ${valor}.");
        
        jogador.Partida.AdicionarRegistro($"{jogador.Nome} deve pagar ${valor}.");
        
        try
        {
            // Debita o valor com desconto
            jogador.Debitar(valor);
            Log.WriteLine($"Transação concluída. Novo saldo de {jogador.Nome}: ${jogador.Dinheiro}");
            jogador.Partida.AdicionarRegistro($"Transação concluída. Novo saldo de {jogador.Nome}: ${jogador.Dinheiro}");
        }
        catch (FundosInsuficientesException)
        {
            // Lógica de falência/hipoteca
            Log.WriteLine($"{jogador.Nome} não conseguiu pagar a multa de ${valor} e deve tomar medidas (hipotecar ou falir).");
            jogador.Partida.AdicionarRegistro($"{jogador.Nome} não conseguiu pagar a multa de ${valor} e deve tomar medidas (hipotecar ou falir).");
            jogador.SetFalido(true);
        }
    }
}
