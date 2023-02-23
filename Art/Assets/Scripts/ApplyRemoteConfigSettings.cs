using UnityEngine;
using UnityEngine.AddressableAssets;

// This script handles the set-up and fetching of the Remote Configuration settings
public class ApplyRemoteConfigSettings : MonoBehaviour
{
    public static ApplyRemoteConfigSettings Instance {get; private set;}
    
    public float characterSize = 1.0f;
    public float characterSpeed = 1.0f;
    public int activeHat = 0;
    
    void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    
    public static void ExitGameplay()
    {
        Addressables.LoadSceneAsync("MainMenu", UnityEngine.SceneManagement.LoadSceneMode.Single, true);
    }

    public static void LoadStore()
    {
        Addressables.LoadSceneAsync("Store", UnityEngine.SceneManagement.LoadSceneMode.Single, true);
    }
}

