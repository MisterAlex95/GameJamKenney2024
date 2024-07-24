using System.Collections;
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
        private bool _canReadNextDialog = false;

        public void StartDialog(IDialog dialog)
        {
            _dialog = dialog;
            ProcessDialog();
            StartCoroutine(EnableNextDialog());
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

                _canReadNextDialog = true;
                DialogManager.Instance.EndDialog();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_canReadNextDialog) return;
            StartCoroutine(EnableNextDialog());
            ProcessDialog();
        }

        private IEnumerator EnableNextDialog()
        {
            _canReadNextDialog = false;
            yield return new WaitForSeconds(0.5f);
            _canReadNextDialog = true;
        }
    }
}