public class EffectGoToJail : IPlayerEffect
{
    public void Execute(Player jogador) 
    {
        jogador.SetPreso(true);
        // jogador.MoverParaPosicao(10); // posição da cadeia
    }
    
}