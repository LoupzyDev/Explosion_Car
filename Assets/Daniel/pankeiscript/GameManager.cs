using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject HUD;
    public GameObject controls;


    // Start is called before the first frame update
    public void Estarp()
    {
        SceneManager.LoadScene(1);

    }


    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        controls.SetActive(false);
    }

    public void ShowOptions()
    {
        pauseMenu.SetActive(false);
        controls.SetActive(true);
    }
    public void RessumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }


    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}