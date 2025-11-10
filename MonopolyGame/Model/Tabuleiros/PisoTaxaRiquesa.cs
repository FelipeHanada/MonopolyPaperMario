using MonopolyGame.Impl.Efeitos;

namespace MonopolyGame.Model.Tabuleiros;

public class PisoTaxaRiquesa(string nome, int taxa) : Piso(nome, new EfeitoDebitoFixo(taxa))
{
    public int Taxa { get; } = taxa;
}
