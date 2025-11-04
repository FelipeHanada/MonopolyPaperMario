using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;

namespace MonopolyGame.Impl.Efeitos;


public class EfeitoTrocaPosicaoRandomico : IEfeitoJogador
{
    public void Aplicar(Jogador jogador)
    {
        if (jogador == null) throw new ArgumentNullException(nameof(jogador));

        var alvosValidos = jogador.Partida.JogadoresAtivos.Where(j => j != jogador).ToList();

        if (!alvosValidos.Any())
        {
            return;
        }

        Random rand = new Random();
        Jogador jogadorAlvo = alvosValidos[rand.Next(alvosValidos.Count)];

        jogador.Partida.Tabuleiro.TrocarPosicao(jogador, jogadorAlvo);
    }
}
