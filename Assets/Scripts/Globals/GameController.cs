using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool isUnloading;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneIndex.GameScene);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneIndex.MenuScene);
    }

    public void RestartGame()
    {
        isUnloading = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
