using MonopolyGame.Utils;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;

namespace MonopolyGame.Impl.Efeitos;


public class EfeitoSairDaCadeia : IEfeitoJogador
{
    public void Aplicar(Jogador jogador)
    {
        if (jogador == null) throw new ArgumentNullException(nameof(jogador));

        if (jogador.Preso)
        {
            jogador.SetPreso(false);
            Log.WriteLine($"{jogador.Nome} está livre da prisão!");
            jogador.Partida.AdicionarRegistro($"{jogador.Nome} está livre da prisão!");
        }
    }
}
