namespace MonopolyGame.Model.Historicos;


public class Historico()
{
    public List<string> Registros { get; } = [];

    public void AddRegistro(string registro)
    {
        Registros.Add(registro);
    }
}
