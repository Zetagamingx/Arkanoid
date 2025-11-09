using UnityEngine;

public class MultiballBrick : SpecialBrick
{
   
    protected override void OnPlayerHit()
    {
        PowerUpManager.instance.MultiBall();
        base.OnPlayerHit();        
    }
}
