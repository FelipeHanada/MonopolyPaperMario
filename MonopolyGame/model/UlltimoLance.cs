namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class UltimoLance
    {
        public Jogador Licitante { get; private set; }
        public int Valor { get; private set; }

        public UltimoLance(Jogador licitante, int valor)
        {
            Licitante = licitante;
            Valor = valor;
        }
    }
}