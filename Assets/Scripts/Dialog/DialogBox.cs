using Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dialog
{
    public class DialogBox : MonoBehaviour, IPointerClickHandler
    {
        public Sprite narratorSprite;
        public string narratorName;

        public TMP_Text dialogText;
        public Image speakerImage;
        public TMP_Text speakerName;
        private IDialog _dialog;

        public void StartDialog(IDialog dialog)
        {
            _dialog = dialog;
            ProcessDialog();
        }

        private void ProcessDialog()
        {
            if (_dialog.HasNextDialog())
            {
                var text = _dialog.GetNextDialog();

                // Narrator detection
                if (text.StartsWith("+"))
                {
                    text = text[1..];
                    speakerName.text = narratorName;
                    speakerImage.sprite = narratorSprite;
                }
                else
                {
                    speakerName.text = _dialog.GetSpeakerName();
                    speakerImage.sprite = _dialog.GetSpeakerSprite();
                }

                dialogText.text = text;
            }
            else
            {
                if (_dialog.ShouldIncreaseStageOnDialogEnd())
                {
                    GameManager.Instance.IncrCharacterState(_dialog.GetCharacterToIncreaseStage());
                }

                if (_dialog.GetTriggerActionName() != TriggerActionName.None)
                {
                    GameManager.Instance.ProcessTriggerAction(_dialog.GetTriggerActionName());
                }

                DialogManager.Instance.EndDialog();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ProcessDialog();
        }
    }
}