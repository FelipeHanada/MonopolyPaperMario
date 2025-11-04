using MonopolyGame.Exceptions;
using MonopolyGame.Interface;
using MonopolyGame.Impl.Efeitos;

namespace MonopolyGame.Model.Partidas;


public class Jogador
{
    public Partida Partida { get; }
    public string Nome { get; set; }
    public int Dinheiro { get; set; }
    public bool Falido { get; set; }
    public bool Preso { get; set; }
    public int TurnosPreso { get; private set; }
    public HashSet<IPosseJogador> Posses { get; }

    // NOVO: Contador de cartas de Passe Livre da Prisão
    public int CartasPasseLivre { get; set; }
    public EfeitoDuplighostReversor? EfeitoDuplighostAtivo { get; set; } // Guarda a referência do efeito
    // Atributos para as cartas e efeitos especiais
    public bool Reverso { get; set; }
    public int Desconto { get; set; }
    public bool PodeComprar { get; set; }
    public Jogador pagador { get; set; }
    public bool TemBoost { get; set; }
    public int QtdATirarNoProximoTurno { get; set; }
    public bool PodeJogar { get; set; }
    public int Multiplicador { get; set; }

    public Jogador(Partida partida, string nome, int dinheiroInicial = 1500)
    {
        Partida = partida;
        Nome = nome;
        Dinheiro = dinheiroInicial;
        Posses = [];
        Falido = false;
        Preso = false;
        TurnosPreso = 0;
        CartasPasseLivre = 0;

        // Inicialização dos novos atributos
        Reverso = false;
        Desconto = 0;
        PodeComprar = true;
        PodeJogar = true;
        pagador = this;
        TemBoost = false;
        EfeitoDuplighostAtivo = null;
        QtdATirarNoProximoTurno = 0;
        Multiplicador = 0;
    }

    public void IncrementarTurnosPreso()
    {
        if (Preso)
        {
            TurnosPreso++;
        }
    }

    public bool RemoverPosse(IPosseJogador posseJogador)
    {
        if (posseJogador.Proprietario != this) return false;
        posseJogador.Proprietario = null;
        Posses.Remove(posseJogador);
        return true;
    }

    public bool AdicionarPosse(IPosseJogador possesJogador)
    {
        if (possesJogador.Proprietario != null) return false;
        possesJogador.Proprietario = this;
        Posses.Add(possesJogador);
        return true;
    }

    //public void TransferirPossePara(Jogador destinatario, IPosseJogador posse)
    //{
    //    if (destinatario == null) throw new ArgumentNullException(nameof(destinatario));
    //    if (posse == null) throw new ArgumentNullException(nameof(posse));
    //    if (posse.Proprietario != this)
    //    {
    //        throw new PosseNaoEDoJogadorCorrenteException(posse, this, "A posse não pertence ao jogador que está tentando transferi-la.");
    //    }

    //    if (Posses.Remove(posse))
    //    {
    //        destinatario.AdicionarPosseJogador(posse);
    //        posse.Proprietario = destinatario;
    //    }
    //}

    public void TransferirDinheiroPara(Jogador destinatario, int valor)
    {
        if (destinatario == null) throw new ArgumentNullException(nameof(destinatario));
        if (valor == 0) return;

        if (valor > 0) // Ofertante (this) paga ao Alvo (destinatario)
        {
            Debitar(valor);
            destinatario.Creditar(valor);
        }
        else // Alvo (destinatario) paga ao Ofertante (this) (valor é negativo)
        {
            destinatario.Debitar(-valor);
            Creditar(-valor);
        }
    }

    public void Creditar(int valor)
    {
        if (valor < 0) throw new ArgumentException("O valor a ser creditado não pode ser negativo.");
        Dinheiro += valor;
    }

    public void Debitar(int valor)
    {
        valor = AplicarDesconto(valor);
        if (valor < 0) throw new ArgumentException("O valor a ser debitado não pode ser negativo.");
        if (Dinheiro < valor)
        {
            // Corrigido: Passa uma string como segundo argumento, não um int.
            throw new FundosInsuficientesException(this, $"Não há fundos suficientes para debitar ${valor}.");
        }
        Dinheiro -= valor;
    }

    public void SetFalido(bool falido)
    {
        Falido = falido;
        if (falido)
        {
            Console.WriteLine($"O jogador {Nome} faliu!");
        }
    }

    public void SetPreso(bool preso)
    {
        Preso = preso;
        if (!preso)
        {
            TurnosPreso = 0;
        }
    }

    public int AplicarDesconto(int valorBase)
    {
        if (Desconto <= 0)
        {
            return valorBase; // Sem desconto, retorna o valor original
        }
        Console.WriteLine("Oba, " + Nome + " teve um desconto no débito.");
    // Calcula o fator de desconto (ex: 30 / 100 = 0.3)
        double fatorDesconto = Desconto / 100.0;

// Calcula o valor final: Valor Base * (1 - Fator de Desconto)
// Usamos Math.Round para garantir que o resultado seja um inteiro (arredondando para o mais próximo).
        int valorFinal = (int)Math.Round(valorBase * (1.0 - fatorDesconto));

// Mensagem de log para facilitar o debug e o feedback ao usuário
        int valorDescontado = valorBase - valorFinal;
        Console.WriteLine($"[Muskular] Despesa de ${valorBase} ajustada para ${valorFinal} (-${valorDescontado} de desconto).");

        return valorFinal;
    }

}