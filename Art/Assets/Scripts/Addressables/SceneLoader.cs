using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Button onStartButton;
    [SerializeField] private Button onStoreButton;
    
    
    void Start()
    {
        
    }

    public void NextScene()
    {
        Addressables.LoadSceneAsync("LoadingScene", UnityEngine.SceneManagement.LoadSceneMode.Single, true);
    }
}
