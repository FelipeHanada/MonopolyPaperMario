namespace MonopolyGame.Model.Historicos;

public class Historico
{
    private List<string> jogadas { get; } = [];

    public void AddJogada(string jogada)
    {
        jogadas.Add(jogada);
    }
}