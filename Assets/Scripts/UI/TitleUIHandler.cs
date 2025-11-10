using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEditor;


public class TitleUIHandler : MonoBehaviour, InputSystem_Actions.IUIActions
{
    [SerializeField] GameObject titleMenu;
    [SerializeField] GameObject startText;
    [SerializeField] GameObject levelSection;
    public TitleSceneModifier titleSceneModify;

    InputSystem_Actions controlsUI;

    private void Awake()
    {
        controlsUI = new InputSystem_Actions();
        controlsUI.UI.SetCallbacks(this);

        titleSceneModify = FindFirstObjectByType<TitleSceneModifier>();
    }

   

    

    public void OnEnable()
    {
        controlsUI.Enable();
    }

    public void OnDisable()
    {
        controlsUI.Disable();
    }

    public void ShowTitleMenu()
    {
        startText.SetActive(false);
        titleMenu.SetActive(true);
    }

    public void NewGame()
    {
        titleSceneModify.ChangeScene("StoneAgeLevel");
    }

    

    public void SelectLevel()
    {
        levelSection.SetActive(true);
        titleMenu.SetActive(false);
    }

    public void ReturnFromLevelSelect()
    {
        titleMenu.SetActive(true);
        levelSection.SetActive(false);
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
        if(context.performed) { ShowTitleMenu(); }
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
    }
}
