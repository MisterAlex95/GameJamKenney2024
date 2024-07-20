using System.Collections.Generic;
using Camera;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(BoxCollider))]
    public class ClickableObjectMoveCamera : MonoBehaviour
    {
        public List<CameraPositionName> canBeTriggerFromCameraPositions;
        public CameraPositionName cameraPositionName;

        private void Start()
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }

        public void OnMouseDown()
        {
            if (!GameManager.Instance.CanInteract()) return;
            if (!canBeTriggerFromCameraPositions.Contains(CameraManager.Instance().GetCameraPosition())) return;
            CameraManager.Instance().SetCameraPosition(cameraPositionName);
        }
    }
}