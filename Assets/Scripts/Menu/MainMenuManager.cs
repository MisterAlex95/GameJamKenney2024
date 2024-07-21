using System;
using EasyTransition;
using UnityEngine;

namespace Menu
{
    public class MainMenuManager : MonoBehaviour
    {
        public TransitionSettings transitionSettings;
        private TransitionManager _transitionManager;

        public GameObject creditPage;

        private void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            _transitionManager = TransitionManager.Instance();
        }

        public void PlayGame()
        {
            _transitionManager.Transition("MailScene", transitionSettings, 0);
        }

        public void OpenCreditPage()
        {
            creditPage.SetActive(true);
        }

        public void CloseCreditPage()
        {
            creditPage.SetActive(false);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}