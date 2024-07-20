using System.Collections.Generic;
using Camera;
using UnityEngine;

namespace Core
{
    public class ClickableObjectRunAnimationAndMoveCamera : MonoBehaviour
    {
        public List<CameraPositionName> canBeTriggerFromCameraPositions;
        public CameraPositionName cameraPositionName;
        public string triggerName;
        public bool state = false;

        // Run animation on click
        public void OnMouseDown()
        {
            if (!canBeTriggerFromCameraPositions.Contains(CameraManager.Instance().GetCameraPosition())) return;
            CameraManager.Instance().SetCameraPosition(cameraPositionName);
            if (triggerName == null) return;
            state = !state;
            GetComponent<Animator>().SetBool(triggerName, state);
        }
    }
}