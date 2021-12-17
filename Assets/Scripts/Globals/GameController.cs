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
        isUnloading = true; // This is so that game objects don't continue destroying or updating themselves, to avoid nulls
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}