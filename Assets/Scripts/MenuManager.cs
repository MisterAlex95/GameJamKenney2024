using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("clicked");
        // Load the next scene in the build index
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
}