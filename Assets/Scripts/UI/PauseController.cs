using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public static bool IsPaused;
    public static bool isPlayerDead;

    public GameObject pausePanel;
    public GameObject resumeButton;
    public GameObject goText;
    public GameObject pauseButton;
    
    // TODO: Fix pause with ESC??

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    void FixedUpdate()
    {
        if (isPlayerDead)
            LoseGame();
    }

    public void Pause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
    }

    public void LoseGame()
    {
        // resumeButton.GetComponent<Button>().interactable = false;
        Pause();
        pausePanel.SetActive(true);
        resumeButton.SetActive(false);
    }

    public void UnsetDeath()
    {
        isPlayerDead = false;
    }
}
