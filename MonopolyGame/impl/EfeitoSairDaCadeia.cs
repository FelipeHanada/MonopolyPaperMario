public class EfeitoSairDaCadeia: IEfeitoJogador
{
    public void Execute(Player jogador) 
    {
        jogador.SetPreso(false);
       
    }
}