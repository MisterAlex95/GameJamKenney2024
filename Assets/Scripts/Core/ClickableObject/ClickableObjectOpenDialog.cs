using Character;
using Dialog;
using UnityEngine;

namespace Core.ClickableObject
{
    public class ClickableObjectOpenDialog : ClickableObject
    {
        public TriggerActionName triggerActionName;
        public DialogData dialogData;
        private bool _alreadyTriggered = false;

        public new void OnMouseDown()
        {
            base.OnMouseDown();
            if (baseReturned) return;
            Debug.Log("coucou 1");
            if (!dialogData.looping && _alreadyTriggered)
            {
                baseReturned = true;
                return;
            }

            Debug.Log("coucou 2");

            if (dialogData.enableDialogAtStage > GameManager.Instance.GetCharacterState(CharacterName.Camden))
            {
                baseReturned = true;
                return;
            }

            Debug.Log("coucou 3");


            DialogManager.Instance.StartDialog(new Dialog.Dialog(dialogData),
                () =>
                {
                    Debug.Log("coucou 4");
                    if (_alreadyTriggered) return;
                    GameManager.Instance.ProcessTriggerAction(triggerActionName);
                    _alreadyTriggered = true;
                });
        }
    }
}