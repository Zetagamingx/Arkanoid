using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] int playerInitialLives;
    public int playerCurrentLives;
    [SerializeField] int playerMaxLives;
    [SerializeField] GameObject playerBall;
    public Vector3 newSpawnPosition;
    public ScoreAndLifeManager scoreAndLifeManager;
    public LevelUIHandler levelUI;

    public static PlayerManager instance { get; private set; }

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
        if (scene.name == "TitleScreen")
        {
            Destroy(gameObject);
        }
        playerCurrentLives = playerInitialLives;
        scoreAndLifeManager = FindFirstObjectByType<ScoreAndLifeManager>();
        levelUI = FindFirstObjectByType<LevelUIHandler>();

        if (playerBall == null)
        {
            playerBall = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

 

    public void LiveCounter(int count)
    {
        
        if (count < 0 && PowerUpManager.instance.isMultiBall) return;

        playerCurrentLives += count;
        playerCurrentLives = Mathf.Clamp(playerCurrentLives, 0, playerMaxLives);

        levelUI.CheckForGameOver();
        scoreAndLifeManager.ChangeCurrentLives();
    }

    
    public void SpawnNewBall()
    {
        if (PowerUpManager.instance.isMultiBall) return;
        AudioManager.instance.PlaySFX("New ball");
        PowerUpManager.instance.activeBalls += 1;
        GameObject newBall = Instantiate(playerBall, newSpawnPosition, Quaternion.identity);
        newBall.SetActive(true);
    }

    public void LevelResetLives()
    {
        playerCurrentLives = playerInitialLives;
    }

    public void Update()
    {
        
    }

}
