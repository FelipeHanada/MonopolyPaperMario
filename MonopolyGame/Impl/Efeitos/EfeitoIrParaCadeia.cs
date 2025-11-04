using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;


public class EfeitoIrParaCadeia(Partida partida) : EfeitoJogador(partida)
{
    public override void Aplicar(Jogador jogador)
    {
        // 1. Verifica se o jogador tem o Passe Livre
        if (jogador.CartasPasseLivre > 0)
        {
            // 2. Lógica de Input para a escolha do jogador
            string? resposta = Console.ReadLine()?.Trim().ToLower();

            if (resposta == "s")
            {
                jogador.CartasPasseLivre--; // Diminui o contador
                
                // IMPORTANTE: Adicione aqui a lógica para DEVOLVER a carta
                // ao monte 'Sorte' ou 'Reves', se o seu jogo usa essa regra.
                
                return; // Encerra a execução: o jogador não vai para a cadeia
            }
        }

        jogador.SetPreso(true);

        GetPartida().GetTabuleiro().MoverJogadorPara(jogador, 10, false);
    }
}
