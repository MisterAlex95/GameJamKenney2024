using System;
using Character;
using Core;
using UnityEngine;

namespace Dialog
{
    public class DialogManager : MonoBehaviour
    {
        public DialogBox dialogBox;
        private Action _onEnd;
        public static DialogManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public bool IsDialogActive => dialogBox.gameObject.activeSelf;

        public void StartDialog(IDialog dialogData, Action onEnd = default)
        {
            if (!GameManager.Instance.CanInteract()) return;

            dialogBox.gameObject.SetActive(true);
            dialogBox.StartDialog(dialogData);
            _onEnd = onEnd;
        }

        public void EndDialog()
        {
            dialogBox.gameObject.SetActive(false);
            _onEnd?.Invoke();
            _onEnd = default;
        }

        public void StartDialogOfCharacter(CharacterName characterName)
        {
            var characters =
                FindObjectsByType<Character.Character>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            var character = Array.Find(characters, c => c.GetCharacterName() == characterName);
            character?.OnMouseDown();
        }
    }
}