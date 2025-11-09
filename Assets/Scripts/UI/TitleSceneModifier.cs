using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneModifier : MonoBehaviour
{
    
    
    
    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    
}
