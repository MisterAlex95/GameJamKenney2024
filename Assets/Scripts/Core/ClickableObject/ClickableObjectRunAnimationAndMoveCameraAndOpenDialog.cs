using Camera;
using Character;
using Dialog;
using UnityEngine;

namespace Core.ClickableObject
{
    public class ClickableObjectRunAnimationAndMoveCameraAndOpenDialog : ClickableObject
    {
        public CameraPositionName cameraPositionName;
        public DialogData dialogData;
        public TriggerActionName triggerActionName;
        private bool _alreadyTriggered = false;
        public string triggerName;
        public bool state = false;

        public new void OnMouseDown()
        {
            base.OnMouseDown();
            if (baseReturned) return;
            CameraManager.Instance.SetCameraPosition(cameraPositionName);
            if (triggerName == null) return;
            state = !state;
            GetComponent<Animator>().SetBool(triggerName, state);

            if (!dialogData.looping && _alreadyTriggered) return;
            if (dialogData.enableDialogAtStage > GameManager.Instance.GetCharacterState(CharacterName.Camden)) return;

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