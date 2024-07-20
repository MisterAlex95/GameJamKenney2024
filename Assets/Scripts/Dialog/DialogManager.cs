using System;
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
    }
}