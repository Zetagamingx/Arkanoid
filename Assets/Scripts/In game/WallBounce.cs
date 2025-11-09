using UnityEngine;

public class WallBounce : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        AudioManager.instance.PlaySFX("Wall bounce");
                    
    }
}
