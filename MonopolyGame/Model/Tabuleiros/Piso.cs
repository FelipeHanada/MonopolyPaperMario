using MonopolyGame.Utils;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Model.Tabuleiros;


public class Piso
{
    public string Nome { get; }
    // Corrigido: Transformado em uma propriedade pública com setter privado
    public IEfeitoJogador? EfeitoAcao { get; private set; }

    // Construtor para pisos sem efeito especial
    public Piso(string nome)
    {
        Nome = nome;
        EfeitoAcao = null;
    }

    // Construtor para pisos com um efeito
    public Piso(string nome, IEfeitoJogador efeito)
    {
        Nome = nome;
        EfeitoAcao = efeito ?? throw new ArgumentNullException(nameof(efeito));
    }

    // Método que ativa o efeito do piso
    public void Efeito(Jogador jogador)
    {
        if (EfeitoAcao != null)
        {
            EfeitoAcao.Aplicar(jogador);
        }
        else
        {
            Log.WriteLine($"{jogador.Nome} parou em '{Nome}', mas nada acontece.");
        }
    }
}