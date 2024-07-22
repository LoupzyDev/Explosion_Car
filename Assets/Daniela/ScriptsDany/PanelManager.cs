using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject pausePanel;
    public void StartGame()
    {
        Time.timeScale = 1f;
        mainMenuPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        mainMenuPanel.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("UIScene");
        pausePanel.SetActive(false);
        mainMenuPanel.SetActive(false);
    }
}
