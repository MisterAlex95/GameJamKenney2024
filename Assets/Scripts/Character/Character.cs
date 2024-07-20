using Core;
using Dialog;
using UnityEngine;

namespace Character
{
    public class Character : MonoBehaviour
    {
        public CharacterData characterData;
        private bool _alreadyTriggered = false;
        private int _currentState = 0;

        private void Start()
        {
            GameManager.Instance.SetCharacterState(characterData.characterName, _currentState);
        }

        private int GetCurrentState()
        {
            var newState = GameManager.Instance.GetCharacterState(characterData.characterName);
            if (newState == _currentState) return _currentState;

            _alreadyTriggered = false;
            _currentState = newState;
            return _currentState;
        }

        public void OnMouseDown()
        {
            if (DialogManager.Instance.IsDialogActive) return;

            if (GetCurrentState() > characterData.dialogs[_currentState].enableDialogAtStage)
            {
                return;
            }

            var dialog = GetCurrentDialog();
            if (dialog == null) return;
            Dialog.DialogManager.Instance.StartDialog(dialog);
            _alreadyTriggered = true;
        }

        private IDialog GetCurrentDialog()
        {
            foreach (var dialog in characterData.dialogs)
            {
                if (dialog.enableDialogAtStage != GetCurrentState()) continue;
                if (!dialog.looping && _alreadyTriggered)
                    continue;

                dialog.speakerName = characterData.characterName;
                return new Dialog.Dialog(dialog);
            }

            return null;
        }
    }
}