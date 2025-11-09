using UnityEngine;

public class GhostBallBrick : SpecialBrick
{
    protected override void OnPlayerHit()
    {
        PowerUpManager.instance.GoThroughBricks();
        base.OnPlayerHit();
    }
}
