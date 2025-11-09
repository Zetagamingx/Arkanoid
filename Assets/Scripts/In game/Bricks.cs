using UnityEngine;
using System.Collections.Generic;

public class Bricks : MonoBehaviour
{
    [SerializeField] private int brickLife;
    [SerializeField] private bool isThickBrick;
    [SerializeField] private List<Material> brickMaterials;
    private Renderer brickRenderer;
    [SerializeField] int brickPoints;
    public ScoreAndLifeManager scoreAndLifeManager;

    private void Awake()
    {
        brickRenderer = GetComponent<Renderer>();
        if (isThickBrick)
        {
            brickRenderer.material = brickMaterials[2];
            brickLife = 3;
            return;
        }
        brickLife = 1;
        brickRenderer.material = brickMaterials[0];
        
    }

    public void Start()
    {
        scoreAndLifeManager = FindFirstObjectByType<ScoreAndLifeManager>();
        WinController.instance.InitialBricks();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        
        if (brickLife == 0) return;

        brickLife -= 1; 

        if (brickLife > 0)
        {
            AudioManager.instance.PlaySFX("Hit thick brick");
            int materialIndex = isThickBrick ? brickLife - 1 : 0;
            brickRenderer.material = brickMaterials[materialIndex];
        }

        else
        {
            AudioManager.instance.PlaySFX("Destroy brick");
            scoreAndLifeManager.IncreaseScore(brickPoints);
            WinController.instance.DestroyBrick();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        AudioManager.instance.PlaySFX("Destroy brick");
        scoreAndLifeManager.IncreaseScore(brickPoints);
        WinController.instance.DestroyBrick();
        Destroy(gameObject);
    }

    private void Update()
    {
        
    }
}
