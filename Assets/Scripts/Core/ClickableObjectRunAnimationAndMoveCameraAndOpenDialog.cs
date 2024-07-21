using System.Collections.Generic;
using Camera;
using Character;
using Dialog;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(BoxCollider))]
    public class ClickableObjectRunAnimationAndMoveCameraAndOpenDialog : MonoBehaviour
    {
        public List<CameraPositionName> canBeTriggerFromCameraPositions;
        public CameraPositionName cameraPositionName;
        public DialogData dialogData;
        public TriggerActionName triggerActionName;
        private bool _alreadyTriggered = false;
        public string triggerName;
        public bool state = false;

        private void Start()
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }

        public void OnMouseDown()
        {
            if (!GameManager.Instance.CanInteract()) return;
            if (!canBeTriggerFromCameraPositions.Contains(CameraManager.Instance().GetCameraPosition())) return;
            CameraManager.Instance().SetCameraPosition(cameraPositionName);
            if (triggerName == null) return;
            state = !state;
            GetComponent<Animator>().SetBool(triggerName, state);

            if (!dialogData.looping && _alreadyTriggered) return;
            if (dialogData.enableDialogAtStage > GameManager.Instance.GetCharacterState(CharacterName.Camden)) return;

            DialogManager.Instance.StartDialog(new Dialog.Dialog(dialogData),
                () =>
                {
                    if (!_alreadyTriggered)
                    {
                        GameManager.Instance.ProcessTriggerAction(triggerActionName);
                    }
                });
            _alreadyTriggered = true;
        }
    }
}