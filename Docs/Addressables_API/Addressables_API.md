# Addressables API
 Addressables API를 아래와 같이 기술합니다.

 - Addressables Scene 비동기 로드      
`
Addressables.LoadSceneAsync("addressable name", UnityEngine.SceneManagement.LoadSceneMode.Single, true);
`      

 - Addressalbes Download 비동기     
`
Addressables.DownloadDependenciesAsync("Level_0" + GameManager.s_CurrentLevel);
`       

 - Addressables Instantiate 비동기       
`
Addressables.InstantiateAsync(hatKey, m_HatAnchor, false);
`      
