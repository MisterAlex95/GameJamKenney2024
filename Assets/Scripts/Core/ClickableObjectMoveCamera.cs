using System.Collections.Generic;
using Camera;
using UnityEngine;

namespace Core
{
    public class ClickableObjectMoveCamera : MonoBehaviour
    {
        public List<CameraPositionName> canBeTriggerFromCameraPositions;
        public CameraPositionName cameraPositionName;

        public void OnMouseDown()
        {
            if (!canBeTriggerFromCameraPositions.Contains(CameraManager.Instance().GetCameraPosition())) return;
            CameraManager.Instance().SetCameraPosition(cameraPositionName);
        }
    }
}