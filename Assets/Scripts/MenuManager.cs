using EasyTransition;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public TransitionSettings transitionSettings;
    private TransitionManager _transitionManager;

    private void Start()
    {
        _transitionManager = TransitionManager.Instance();
    }

    public void PlayGame()
    {
        _transitionManager.Transition("LetterScene", transitionSettings, 0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}