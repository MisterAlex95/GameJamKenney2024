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

        private void Start()
        {
            dialogBox.gameObject.SetActive(false);
        }

        public void StartDialog(Character.Character character)
        {
            dialogBox.gameObject.SetActive(true);
            dialogBox.SetDialog(character);
            dialogBox.GetNextDialog();
        }

        public void EndDialog()
        {
            dialogBox.gameObject.SetActive(false);
        }
    }
}