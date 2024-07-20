using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dialog
{
    public class DialogBox : MonoBehaviour, IPointerClickHandler
    {
        public TMP_Text dialogText;
        public Image speakerImage;
        public TMP_Text speakerName;
        private IDialog _dialog;


        public void StartDialog(IDialog dialog)
        {
            _dialog = dialog;
            speakerName.text = dialog.GetSpeakerName();
            speakerImage.sprite = dialog.GetSpeakerSprite();
            dialogText.text = dialog.GetNextDialog();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_dialog.HasNextDialog())
            {
                dialogText.text = _dialog.GetNextDialog();
            }
            else
            {
                DialogManager.Instance.EndDialog();
            }
        }
    }
}