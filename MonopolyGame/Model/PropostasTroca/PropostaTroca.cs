using MonopolyGame.Interface;
using MonopolyGame.Interface.PropostasTroca;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Utils;

namespace MonopolyGame.Model.PropostasTroca;


public class PropostaTroca(Jogador? ofertante, Jogador destinatario) : IPropostaTroca
{
    public Jogador? Ofertante { get; } = ofertante;
    public Jogador Destinatario { get; } = destinatario;
    public List<IPosseJogador> PossesOfertadas { get; } = [];
    public List<IPosseJogador> PossesDesejadas { get; } = [];
    public int DinheiroOfertado { get; set; } = 0;

    public bool Valido()
    {
        if (Ofertante != null)
        {
            foreach (IPosseJogador posseJogador in PossesOfertadas)
            {
                if (posseJogador.Proprietario != Ofertante) return false;
            }
        }

        foreach (IPosseJogador posseJogador in PossesDesejadas)
        {
            if (posseJogador.Proprietario != Destinatario) return false;
        }

        if (DinheiroOfertado >= 0)
        {
            return Ofertante == null || Ofertante?.Dinheiro >= DinheiroOfertado;
        } else
        {
            return Destinatario.Dinheiro >= -DinheiroOfertado;
        }
    }

    public bool Efetuar()
    {
        if (!Valido()) return false;
        
        foreach (IPosseJogador posseJogador in PossesDesejadas)
        {
            Log.WriteLine("Removendo posse " + posseJogador.Nome + " de " + Destinatario.Nome);
            if (Ofertante != null) Log.WriteLine("Adicionando posse " + posseJogador.Nome + " em " + Ofertante.Nome);
            Destinatario.RemoverPosse(posseJogador);
            Ofertante?.AdicionarPosse(posseJogador);
        }

        foreach (IPosseJogador posseJogador in PossesOfertadas)
        {
            if (Ofertante != null) Log.WriteLine("Removendo posse " + posseJogador.Nome + " de " + Ofertante.Nome);
            Log.WriteLine("Adicionando posse " + posseJogador.Nome + " em " + Destinatario.Nome);
            Ofertante?.RemoverPosse(posseJogador);
            Destinatario.AdicionarPosse(posseJogador);
        }

        Log.WriteLine("DinheiroOfertado = " + DinheiroOfertado);
        Ofertante?.Dinheiro -= DinheiroOfertado;
        Destinatario.Dinheiro += DinheiroOfertado;
        
        return true;
    }
}
