using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanging : MonoBehaviour
{
    private const int DefaultStartSceneIndex = 1;
    public void LoadNextScene()
    {
        var sceneToLoadIndex = DefaultStartSceneIndex;
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            sceneToLoadIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        SceneManager.LoadScene(sceneToLoadIndex);
    }
    public void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
