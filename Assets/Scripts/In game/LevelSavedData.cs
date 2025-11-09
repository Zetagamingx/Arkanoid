using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSavedData : MonoBehaviour
{
    public static LevelSavedData instance { get; private set; }

    private int savedScorePoints;
    private int savedLivesAmmount;
    private ScoreAndLifeManager scoreAndLife;
    private bool hasSavedData = false;

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

        scoreAndLife = FindFirstObjectByType<ScoreAndLifeManager>();
        
    }



    public void SaveScoreAndLife()
    {
        savedScorePoints = scoreAndLife.playerBrickPoints;
        savedLivesAmmount = scoreAndLife.currentLives;
        hasSavedData = true;
    }

    public void LoadSavedData()
    {
        if (!hasSavedData) { return; }
        if (scoreAndLife != null)
        {
            Debug.Log("no deberias pasar aqui de primerazo");
            scoreAndLife.playerBrickPoints = savedScorePoints;
            scoreAndLife.currentLives = savedLivesAmmount;
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
