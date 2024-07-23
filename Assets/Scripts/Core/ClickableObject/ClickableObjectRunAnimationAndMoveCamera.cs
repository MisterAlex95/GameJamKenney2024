using Camera;
using UnityEngine;

namespace Core.ClickableObject
{
    public class ClickableObjectRunAnimationAndMoveCamera : ClickableObjectMoveCamera
    {
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
        }
    }
}