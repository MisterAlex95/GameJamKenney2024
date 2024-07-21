using System.Collections.Generic;
using Camera;
using Character;
using Dialog;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(BoxCollider))]
    public class ClickableObjectOpenDialog : MonoBehaviour
    {
        public List<CameraPositionName> canBeTriggerFromCameraPositions;
        public TriggerActionName triggerActionName;
        public DialogData dialogData;
        private bool _alreadyTriggered = false;

        private void Start()
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }

        public void OnMouseDown()
        {
            if (!GameManager.Instance.CanInteract()) return;
            if (!canBeTriggerFromCameraPositions.Contains(CameraManager.Instance().GetCameraPosition())) return;

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