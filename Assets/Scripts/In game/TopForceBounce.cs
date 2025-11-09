using UnityEngine;

public class TopForceBounce : MonoBehaviour
{
    
    [SerializeField] private float boostY = -1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody rb = collision.rigidbody;
        if (rb == null) return;

        
        rb.AddForce(Vector3.up * boostY, ForceMode.VelocityChange);
    }
    
}
