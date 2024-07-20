using Camera;
using UnityEngine;

namespace Core
{
    public class ClickableObjectRunAnimationAndMoveCamera : MonoBehaviour
    {
        public CameraPositionName cameraPositionName;
        public string triggerName;
        public bool state = false;

        // Run animation on click
        public void OnMouseDown()
        {
            CameraManager.Instance().SetCameraPosition(cameraPositionName);
            if (triggerName == null) return;
            state = !state;
            GetComponent<Animator>().SetBool(triggerName, state);
        }
    }
}