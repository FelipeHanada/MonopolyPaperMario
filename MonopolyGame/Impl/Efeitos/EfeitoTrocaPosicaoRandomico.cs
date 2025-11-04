using MonopolyGame.Model.Partidas;
using MonopolyGame.Interface.Efeitos;

namespace MonopolyGame.Impl.Efeitos;


public class EfeitoTrocaPosicaoRandomico(Partida partida) : EfeitoJogador(partida)
{
    public override void Aplicar(Jogador jogadorSolicitante)
    {
        if (jogadorSolicitante == null) throw new ArgumentNullException(nameof(jogadorSolicitante));

        var alvosValidos = GetPartida().GetJogadoresAtivos().Where(j => j != jogadorSolicitante).ToList();

        if (!alvosValidos.Any())
        {
            return;
        }

        Random rand = new Random();
        Jogador jogadorAlvo = alvosValidos[rand.Next(alvosValidos.Count)];
        
        GetPartida().GetTabuleiro().TrocarPosicao(jogadorSolicitante, jogadorAlvo);
    }
}