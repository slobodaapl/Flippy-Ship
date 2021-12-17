using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool IsPaused;
    public static bool isPlayerDead;

    public GameObject pausePanel;
    public GameObject resumeButton;
    public GameObject goText;
    public GameObject pauseButton;

    private void Update() // Esc to pause only for Desktops. Won't be a thing in android. Not important functionality
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();

            if (pauseButton.activeSelf) pauseButton.SetActive(false);
            else pauseButton.SetActive(true);
            if (pausePanel.activeSelf) pausePanel.SetActive(false);
            else pausePanel.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if (isPlayerDead) // isPlayerDead is changed by player
            LoseGame();
    }

    public void Pause() // stop time on pause
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
    }

    public void LoseGame() // Show pausepanel without resume, behind which is hiding 'Game Over'
    {
        Pause();
        pausePanel.SetActive(true);
        resumeButton.SetActive(false);
    }

    public void UnsetDeath() // When we restart
    {
        isPlayerDead = false;
    }
}