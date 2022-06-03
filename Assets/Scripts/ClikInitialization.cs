using Tabtale.TTPlugins;
using UnityEngine;

public class ClikInitialization : MonoBehaviour
{
    public static ClikInitialization Instance { get; private set; }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        
        TTPCore.Setup();
    }
}