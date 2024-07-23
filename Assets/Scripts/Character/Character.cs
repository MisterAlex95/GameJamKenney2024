using System;
using Core;
using Dialog;
using UnityEngine;

namespace Character
{
    public class Character : MonoBehaviour
    {
        public GameObject characterEmote;
        public CharacterData characterData;
        private bool _alreadyTriggered = false;
        private int _currentState = 0;

        private void Start()
        {
            GameManager.Instance.SetCharacterState(characterData.characterName, _currentState);
            characterEmote.SetActive(false);
        }

        private int GetCurrentState()
        {
            var newState = GameManager.Instance.GetCharacterState(characterData.characterName);
            if (newState == _currentState) return _currentState;

            _alreadyTriggered = false;
            _currentState = newState;
            return _currentState;
        }

        public CharacterName GetCharacterName()
        {
            return characterData.characterName;
        }

        public void OnMouseDown()
        {
            if (DialogManager.Instance.IsDialogActive) return;

            var dialog = GetCurrentDialog();
            if (dialog == null) return;
            _alreadyTriggered = true;
            Dialog.DialogManager.Instance.StartDialog(dialog);
        }

        private IDialog GetCurrentDialog()
        {
            foreach (var dialog in characterData.dialogs)
            {
                if (dialog.enableDialogAtStage != GetCurrentState()) continue;
                if (!dialog.looping && _alreadyTriggered)
                    continue;

                dialog.speakerName = characterData.characterName.GetString();
                return new Dialog.Dialog(dialog);
            }

            return null;
        }

        private bool HaveDialog()
        {
            foreach (var dialog in characterData.dialogs)
            {
                if (dialog.enableDialogAtStage == GetCurrentState() && !dialog.looping && !_alreadyTriggered)
                    return true;
            }

            return false;
        }

        private void Update()
        {
            characterEmote.SetActive(HaveDialog());
        }
    }
}