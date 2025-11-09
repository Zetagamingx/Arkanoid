using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreAndLifeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerScore;
    [SerializeField] TextMeshProUGUI playerLives;
    public int playerBrickPoints;
    public int currentLives;

    

    

    public void Awake()
    {        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TitleScreen")
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Start()
    {
        if (PlayerManager.instance != null)
        {
            Debug.Log("estoy agarrando las vidas actuales de player manager");
            currentLives = PlayerManager.instance.playerCurrentLives;
            playerBrickPoints = PlayerManager.instance.scoreAndLifeManager?.playerBrickPoints ?? 0;
        }

        
        if (LevelSavedData.instance != null)
        {
            Debug.Log("voy a pasar los datos a levelsaveddata");
            LevelSavedData.instance.LoadSavedData();
        }
            
    }

    public void IncreaseScore(int points)
    {
        playerBrickPoints += points;
    }

    public void LevelResetScore()
    {
        playerBrickPoints = 0;
    }

    public void ChangeCurrentLives()
    {
        currentLives = PlayerManager.instance.playerCurrentLives;
    }
    public void Update()
    {        
        playerScore.text = "Score: " + playerBrickPoints;
        playerLives.text = "x   " + currentLives;
    }

  
}
