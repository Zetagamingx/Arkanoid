using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager instance { get; private set;}
    public GameObject levelBall;
    public bool isMultiBall;
    public int activeBalls;
    private BrickStateManager brickStateScript;
    [SerializeField] Vector3 spawnPosition;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isMultiBall = false;
        StopAllCoroutines();

        if (scene.name == "TitleScreen")
        {
            Destroy(gameObject);
        }
        levelBall = GameObject.FindGameObjectWithTag("Player");
        
        brickStateScript = FindFirstObjectByType<BrickStateManager>();
        
        if (brickStateScript == null) Debug.LogWarning("This level does not have brickstatemanager");

        

        
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Start()
    {
        activeBalls = 1;
    }
    public void MultiBall()
    {
        activeBalls += 3;
        StartCoroutine(SpawnBalls());
    }

    public IEnumerator SpawnBalls()
    {
        float spawnInterval = 0.5f;
        int multiplyAmmount = 3;
        Vector3 spawnPosition = new Vector3(0f, -2f, 20f);

        for (int i = 0; i < multiplyAmmount; i++)
        {
            Instantiate(levelBall, spawnPosition, Quaternion.identity);
            
            AudioManager.instance.PlaySFX("New ball");
            yield return new WaitForSeconds(spawnInterval);
        }
        
        isMultiBall = true;
    }

    public void DecreasedBalls()
    {
        activeBalls--;
    }    

    public void Update()
    {
        if (activeBalls < 2)
        {
            isMultiBall= false;
        }
    }

    public void GoThroughBricks()
    {
        StartCoroutine(ToggleBricks());
    }

    public IEnumerator ToggleBricks()
    {
        brickStateScript.ActivateGhostMode();

        yield return new WaitForSeconds(6);

        while (levelBall.transform.position.y > 0)
        {
            yield return null;
        }

        brickStateScript.DeactivateGhostMode();
    }
}
