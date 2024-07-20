using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dialog
{
    public class DialogBox : MonoBehaviour, IPointerClickHandler
    {
        private TMP_Text _dialogText;
        private Character.Character _character;
        private IDialog _dialog;

        private void Awake()
        {
            _dialogText = GetComponentInChildren<TMP_Text>();
        }


        public void StartDialog(IDialog dialog)
        {
            _dialog = dialog;
            _dialogText.text = dialog.GetNextDialog();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_dialog.HasNextDialog())
            {
                _dialogText.text = _dialog.GetNextDialog();
            }
            else
            {
                DialogManager.Instance.EndDialog();
            }
        }
    }
}