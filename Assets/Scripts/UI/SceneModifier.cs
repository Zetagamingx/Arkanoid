using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class SceneModifier : MonoBehaviour
{
    public static SceneModifier instance { get; private set; }
    private LevelSavedData dataSaveScript;
    public ScoreAndLifeManager scoreAndLifeManager;
    public LevelUIHandler levelUI;
    private bool destroyOnNextFrame = false;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            
            Destroy(gameObject);
            return;
        }

        instance = this;

        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TitleScreen")
        {
            
            SceneManager.sceneLoaded -= OnSceneLoaded;
            destroyOnNextFrame = true;
            return; 
        }

        dataSaveScript = FindFirstObjectByType<LevelSavedData>();
        scoreAndLifeManager = FindFirstObjectByType<ScoreAndLifeManager>();
        levelUI = FindFirstObjectByType<LevelUIHandler>();

    }

    public void Update()
    {
        if (destroyOnNextFrame)
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            dataSaveScript.SaveScoreAndLife();
            SceneManager.LoadScene(nextIndex);

        }
        else
        {
            SceneModifier.instance.ChangeScene("TitleScreen");
        }
    }



    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }    

    public void Restart()
    {
        levelUI.LevelRestarted();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerManager.instance.LevelResetLives();
        scoreAndLifeManager.LevelResetScore();
        Time.timeScale = 1f;
    }    


}
