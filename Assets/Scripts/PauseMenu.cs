using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool paused = false;
    
    public GameObject finishPanel;
    public TMP_Text crystalText;

    private int totalCrystals = 12;
    private int collectedCrystals = 13;

    public void ShowFinish(int collected, int total)
    {
        collectedCrystals = collected;
        totalCrystals = total;

        crystalText.text = $"Crystals: {collectedCrystals} / {totalCrystals}";
        finishPanel.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }        
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void loadMenu()
    {
        Time.timeScale = 1f;
        SceneController.instance.loadScene("Menu");
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f; // На случай, если игра на паузе
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pauseMenu.SetActive(false);
        paused = false;
    }

    public void LoadNextLevel()
    {
        SceneController.instance.nextLevel();
    }
    
}
