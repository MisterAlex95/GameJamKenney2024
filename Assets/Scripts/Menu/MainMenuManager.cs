using System;
using EasyTransition;
using Localization;
using UnityEngine;

namespace Menu
{
    public class MainMenuManager : MonoBehaviour
    {
        public TransitionSettings transitionSettings;
        private TransitionManager _transitionManager;

        public GameObject creditPage;
        public GameObject optionPage;

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

        public void OpenOptionPage()
        {
            optionPage.SetActive(true);
        }

        public void CloseOptionPage()
        {
            optionPage.SetActive(false);
        }

        public void SetLanguageFrench() => LocalizationManager.Instance.SetLanguage(LocalizationLanguage.French);
        public void SetLanguageEnglish() => LocalizationManager.Instance.SetLanguage(LocalizationLanguage.English);
        public void SetLanguageNellouche() => LocalizationManager.Instance.SetLanguage(LocalizationLanguage.Nellouche);
        public void SetLanguageMarouche() => LocalizationManager.Instance.SetLanguage(LocalizationLanguage.Marouche);

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}