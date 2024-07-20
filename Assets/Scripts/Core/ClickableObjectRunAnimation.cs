using System.Collections.Generic;
using Camera;
using Dialog;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(BoxCollider))]
    public class ClickableObjectRunAnimation : MonoBehaviour
    {
        public List<CameraPositionName> canBeTriggerFromCameraPositions;
        public string triggerName;
        public bool state = false;

        private void Start()
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }

        public void OnMouseDown()
        {
            if (DialogManager.Instance.IsDialogActive) return;
            if (triggerName == null) return;
            if (!canBeTriggerFromCameraPositions.Contains(CameraManager.Instance().GetCameraPosition())) return;
            state = !state;
            GetComponent<Animator>().SetBool(triggerName, state);
        }
    }
}