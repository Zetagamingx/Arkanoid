using UnityEngine;

public class BallMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Vector3 velocity;
    [SerializeField] float movementSpeed;
    [SerializeField] float paddleSteering;

    private void Awake()
    {
        int ballLayer = LayerMask.NameToLayer("Ball");
        Physics.IgnoreLayerCollision(ballLayer, ballLayer, true);
    }
    void Start()
    {
        
        velocity.x = Random.value < 0.5f ? -1f : 1f;
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(velocity.x, velocity.y, 0f).normalized * movementSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("DeathLine")) return;

        PlayerManager.instance.LiveCounter(-1);
        PowerUpManager.instance.DecreasedBalls();

        if (gameObject.name.Contains("(Clone)"))
        {
            if (PowerUpManager.instance.activeBalls < 2) 
                PlayerManager.instance.SpawnNewBall();

            Destroy(gameObject);
        }
        else
        {
            PlayerManager.instance.SpawnNewBall(); 
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!collision.gameObject.CompareTag("Paddle")) return;
        
        AudioManager.instance.PlaySFX("Hit paddle");

        ContactPoint contact = collision.GetContact(0);
        Transform paddle = collision.transform;

        float offset = (contact.point.x - paddle.position.x) / (paddle.localScale.x / 2f);

        Vector3 newDir = new Vector3(offset * paddleSteering, 1f, 0f).normalized;

        rb.linearVelocity = newDir * movementSpeed;
        
    }
    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > 0f)
        {
            Vector3 v = rb.linearVelocity;
                        
            if (Mathf.Abs(v.y) < 0.5f)
            {
                v.y = Mathf.Sign(v.y) != 0 ? Mathf.Sign(v.y) * 0.5f : 0.5f;
                rb.linearVelocity = v.normalized * movementSpeed;
            }
        }
    }
}
