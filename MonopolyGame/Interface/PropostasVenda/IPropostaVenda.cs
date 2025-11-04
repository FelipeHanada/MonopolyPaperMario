using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Interface.PropostasVenda;


public interface IPropostaVenda
{
    Jogador? GetVendedor();
    Jogador GetComprador();
    List<IPosseJogador> GetPossesVendedor();
    List<IPosseJogador> GetPossesComprador();
    int GetDeltaDinheiro();
    void AddPosseVendedor(IPosseJogador posse);
    void AddPosseComprador(IPosseJogador posse);
    void AddDinheiroVendedor(int dinheiro);
    void AddDinheiroComprador(int dinheiro);
    void SetDeltaDinheiro(int novoDeltaDinheiro);
    bool Valido(); // checa se os jogadores possuem as posses e se tem dinheiro suficiente
}
