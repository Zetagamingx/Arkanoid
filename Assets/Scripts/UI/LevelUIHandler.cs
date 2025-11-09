using UnityEditor;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelUIHandler : MonoBehaviour, InputSystem_Actions.IUIActions
{
    

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject nextLevelMenu;
    InputSystem_Actions controlsUI;

    private void Awake()
    {
        controlsUI = new InputSystem_Actions();
        controlsUI.UI.SetCallbacks(this);
        

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TitleScreen")
        {
            Destroy(gameObject);
        }

        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas != null)
        {
            nextLevelMenu = canvas.transform.Find("NextLevelMenu")?.gameObject;
            pauseScreen = canvas.transform.Find("PauseScreen")?.gameObject;
            gameOverScreen = canvas.transform.Find("GameOverScreen")?.gameObject;
        }

        if (nextLevelMenu == null)
            Debug.LogWarning("NextLevelMenu not found in scene " + scene.name);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public void OnEnable()
    {
        controlsUI.Enable();
    }

    public void OnDisable()
    {
        controlsUI.Disable();
    }

    public void CheckForGameOver()
    {
        if (PlayerManager.instance.playerCurrentLives > 0) return;
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

    public void GoToNextLevel()
    {
        Time.timeScale = 1f;
        nextLevelMenu.SetActive(false);
        SceneModifier.instance.LoadNextLevel();
    }    

    public void LevelRestarted()
    {
        Time.timeScale = 1f;
        gameOverScreen.SetActive(false);
    }

    public void NextLevelScreen()
    {
        Time.timeScale = 0f;
        nextLevelMenu.SetActive(true);
    }

    public void PauseSwitch()
    {
        if (Time.timeScale == 0f) { Time.timeScale = 1f; }
        else
        {
            Time.timeScale = 0f;
        }
        Debug.Log("im pressing pause");
        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1f;
        SceneModifier.instance.ChangeScene("TitleScreen");
    }
    
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
    }

    public void OnClick(InputAction.CallbackContext context)
    {
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) PauseSwitch();
    }
}
