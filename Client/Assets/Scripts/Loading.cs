using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Loading : MonoBehaviour
{
    private AsyncOperationHandle playerHandle;

    private void OnEnable()
    {
        AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync("PreLoad");
        Debug.Log("Download Size" + getDownloadSize);
        playerHandle = Addressables.DownloadDependenciesAsync("PreLoad");
        playerHandle.Completed += OnPlayerLoaded;
    }
    
    // 플레이어가 로드 됬을 때
    private void OnPlayerLoaded(AsyncOperationHandle handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("로드 성공");

            // Addressables.InitializeAsync("Assets/ART/Prefabs/Player.prefab");
        }
    }
}
