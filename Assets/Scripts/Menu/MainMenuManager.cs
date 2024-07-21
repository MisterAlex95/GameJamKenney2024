using EasyTransition;
using UnityEngine;

namespace Menu
{
    public class MainMenuManager : MonoBehaviour
    {
        public TransitionSettings transitionSettings;
        private TransitionManager _transitionManager;

        private void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            _transitionManager = TransitionManager.Instance();
        }

        public void PlayGame()
        {
            _transitionManager.Transition("MailScene", transitionSettings, 0);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}