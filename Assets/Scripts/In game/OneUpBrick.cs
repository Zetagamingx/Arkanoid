using UnityEngine;

public class OneUpBrick : SpecialBrick
{
    protected override void OnPlayerHit()
    {
        AudioManager.instance.PlaySFX("One up");
        PlayerManager.instance.LiveCounter(1);
        base.OnPlayerHit();
    }
}
