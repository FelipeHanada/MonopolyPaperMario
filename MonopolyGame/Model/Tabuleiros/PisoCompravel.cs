using MonopolyGame.Impl.Efeitos;
using MonopolyGame.Model.PossesJogador;

namespace MonopolyGame.Model.Tabuleiros;

public class PisoCompravel(string nome, Propriedade propriedade) : Piso(nome, new EfeitoPropriedadeCompravel(propriedade))
{
    public Propriedade Propriedade { get; } = propriedade;
}
