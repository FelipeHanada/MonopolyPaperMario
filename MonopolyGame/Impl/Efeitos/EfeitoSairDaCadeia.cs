using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;

namespace MonopolyGame.Impl.Efeitos;


public class EfeitoSairDaCadeia(Partida partida) : EfeitoJogador(partida)
{
    override public void Aplicar(Jogador jogador)
    {
        if (jogador == null) throw new ArgumentNullException(nameof(jogador));

        if (jogador.Preso)
        {
            jogador.SetPreso(false);
            Console.WriteLine($"{jogador.Nome} está livre da prisão!");
        }
    }
}