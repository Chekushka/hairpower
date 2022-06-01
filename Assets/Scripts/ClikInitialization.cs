using Tabtale.TTPlugins;
using UnityEngine;

public class ClikInitialization : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        TTPCore.Setup();
    }
}