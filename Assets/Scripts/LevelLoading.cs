using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoading : MonoBehaviour
{
    [SerializeField] private bool isDebug;
    [Scene]
    public string debugBootScene;

    private const string LastSceneIndexKey = "LastScene_Index";
    
    private void Start()
    {
        if(isDebug)
            SceneManager.LoadScene(debugBootScene);
        else
            SceneManager.LoadScene(PlayerPrefs.GetInt(LastSceneIndexKey));
    }

}
