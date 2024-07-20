using Core;
using Dialog;
using UnityEngine;

namespace Character
{
    public class Character : MonoBehaviour
    {
        public CharacterData characterData;
        private int _currentDialogIndex = 0;
        private bool _isDialogActive = false;

        private void Start()
        {
            GameManager.Instance.SetCharacterState(characterData.characterName, 0);
        }

        public int GetCurrentState()
        {
            return GameManager.Instance.GetCharacterState(characterData.characterName);
        }

        public void OnMouseDown()
        {
            if (_isDialogActive) return;

            if (GetCurrentState() >= characterData.dialogs.Length)
            {
                return;
            }

            _currentDialogIndex = 0;
            Dialog.DialogManager.Instance.StartDialog(this);
            _isDialogActive = true;
        }

        private DialogData GetCurrentDialog()
        {
            return characterData.dialogs[GetCurrentState()];
        }

        public bool HasNextDialog()
        {
            if ((GetCurrentDialog().dialogText.Count - _currentDialogIndex) > 0 &&
                GetCurrentState() >= GetCurrentDialog().enableDialogAtStage)
            {
                return true;
            }

            _isDialogActive = false;
            _currentDialogIndex = 0;
            return false;
        }

        public string GetNextDialog()
        {
            if (!HasNextDialog()) return null;
            var dialog = GetCurrentDialog().dialogText[_currentDialogIndex];
            _currentDialogIndex++;

            return dialog;
        }
    }
}