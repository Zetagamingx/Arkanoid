using UnityEngine;
using UnityEngine.SceneManagement;
public class WinController : MonoBehaviour
{
    public static WinController instance { get; private set; }
    public LevelUIHandler levelUI;

    public int brickAmmount;

    public void Awake()
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
        levelUI = FindFirstObjectByType<LevelUIHandler>();
        brickAmmount = 0;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void InitialBricks()
    {
        brickAmmount += 1;
    }

    public void DestroyBrick()
    {
        brickAmmount -= 1;
        if (brickAmmount < 1)
        {
            levelUI.NextLevelScreen();
        }
    }
}
