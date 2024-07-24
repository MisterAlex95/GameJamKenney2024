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
            if (BaseReturned) return;
            if (!dialogData.looping && _alreadyTriggered)
            {
                BaseReturned = true;
                return;
            }

            if (dialogData.enableDialogAtStage > GameManager.Instance.GetCharacterState(CharacterName.Camden))
            {
                BaseReturned = true;
                return;
            }

            DialogManager.Instance.StartDialog(new Dialog.Dialog(dialogData),
                () =>
                {
                    if (_alreadyTriggered) return;
                    GameManager.Instance.ProcessTriggerAction(triggerActionName);
                    _alreadyTriggered = true;
                });
        }
    }
}