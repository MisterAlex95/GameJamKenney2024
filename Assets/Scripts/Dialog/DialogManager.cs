using Core;
using UnityEngine;

namespace Dialog
{
    public class DialogManager : MonoBehaviour
    {
        public DialogBox dialogBox;
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

        public void StartDialog(IDialog dialogData)
        {
            if (!GameManager.Instance.CanInteract()) return;

            dialogBox.gameObject.SetActive(true);
            dialogBox.StartDialog(dialogData);
        }

        public void EndDialog()
        {
            dialogBox.gameObject.SetActive(false);
        }
    }
}