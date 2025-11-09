using UnityEngine;

public class SpecialBrick : MonoBehaviour
{
    protected virtual void OnPlayerHit()
    {
        Destroy(gameObject); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPlayerHit(); 
        }
    }
}
