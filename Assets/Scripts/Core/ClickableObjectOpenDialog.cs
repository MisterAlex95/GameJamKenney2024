using System.Collections.Generic;
using Camera;
using Dialog;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(BoxCollider))]
    public class ClickableObjectOpenDialog : MonoBehaviour
    {
        public List<CameraPositionName> canBeTriggerFromCameraPositions;
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
            DialogManager.Instance.StartDialog(new Dialog.Dialog(dialogData));
            _alreadyTriggered = true;
        }
    }
}