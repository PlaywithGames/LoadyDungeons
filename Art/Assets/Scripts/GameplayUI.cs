using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField]
    private Button m_ExitButton;
 
    [SerializeField]
    private Image m_KeyIndicator;
    private void OnEnable()
    {
        m_ExitButton.onClick.AddListener(ApplyRemoteConfigSettings.ExitGameplay);
    }

    private void OnDisable()
    {
        m_ExitButton.onClick.RemoveListener(ApplyRemoteConfigSettings.ExitGameplay);
    }

    public void KeyCollected()
    {
        m_KeyIndicator.color = Color.white;
    }
}
