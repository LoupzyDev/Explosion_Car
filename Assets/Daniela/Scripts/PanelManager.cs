using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject pausePanel;
    public GameObject gamePanel;

    public void StartGame()
    {
        Time.timeScale = 1f;
        mainMenuPanel.SetActive(false);
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        gamePanel.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        mainMenuPanel.SetActive(false);
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("UIScene");
        Time.timeScale = 1f;
        gamePanel.SetActive(true);
    }
}
