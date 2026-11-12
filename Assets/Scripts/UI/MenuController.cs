using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _mainSettingsPanel;

    public void Awake()
    {
        _mainSettingsPanel.SetActive(false);
    }
    public void StartGame()
    {
        _mainMenuPanel.SetActive(false);
        _mainSettingsPanel.SetActive(false);
    }
    public void MenuPanel()
    {
        _mainMenuPanel.SetActive(true);
        _mainSettingsPanel.SetActive(false);
    }
    public void SettingsPanel()
    {
        _mainSettingsPanel.SetActive(true);
        _mainMenuPanel.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
