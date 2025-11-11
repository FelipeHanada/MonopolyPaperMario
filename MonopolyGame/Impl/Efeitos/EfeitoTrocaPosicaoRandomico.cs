using MonopolyGame.Utils;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;


public class EfeitoTrocaPosicaoRandomico : IEfeitoJogador
{
    public void Aplicar(Jogador jogador)
    {
        if (jogador == null) throw new ArgumentNullException(nameof(jogador));

        var alvosValidos = jogador.Partida.JogadoresAtivos.Where(j => j != jogador).ToList();

        if (!alvosValidos.Any())
        {
            Log.WriteLine($"[AVISO] Efeito Troca Posição: Não há outros jogadores no jogo para trocar.");
            return;
        }

        Random rand = new Random();
        Jogador jogadorAlvo = alvosValidos[rand.Next(alvosValidos.Count)];
        Log.WriteLine($"Efeito: {jogador.Nome} (você) encontrou um cano! Trocando de posição com {jogadorAlvo.Nome}.");
        jogador.Partida.AdicionarRegistro($"Efeito: {jogador.Nome} (você) encontrou um cano! Trocando de posição com {jogadorAlvo.Nome}.");

        jogador.Partida.Tabuleiro.TrocarPosicao(jogador, jogadorAlvo);
    }
}
