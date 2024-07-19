using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the next scene in the build index
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
}