public class EffectLeaveJail: IPlayerEffect
{
    public void Execute(Player jogador) 
    {
        jogador.SetPreso(false);
       
    }
}