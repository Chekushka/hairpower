using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSaving : MonoBehaviour
{
    public static LevelSaving Instance { get; private set; }
    
    private const string LastSceneIndexKey = "LastScene_Index";
    private const int DefaultFirstSceneIndex = 1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        
        if(!PlayerPrefs.HasKey(LastSceneIndexKey))
            PlayerPrefs.SetInt(LastSceneIndexKey, DefaultFirstSceneIndex);
    }

    public void SetNextLevelToLastSaved()
    {
        var indexToSave = DefaultFirstSceneIndex;
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            indexToSave = SceneManager.GetActiveScene().buildIndex + 1;
        
        PlayerPrefs.SetInt(LastSceneIndexKey, indexToSave);
    }

    [Button()]
    private void ResetEditorSaves() => PlayerPrefs.DeleteKey(LastSceneIndexKey);
}
