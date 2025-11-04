using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Interface.Leiloes;


public interface ILeilao
{
    List<Jogador> GetJogadoresParticipantes();
    Jogador GetJogadorAtual();
    int GetUltimoLance();
    void DarLance(int delta);
    void Desistir();
}
