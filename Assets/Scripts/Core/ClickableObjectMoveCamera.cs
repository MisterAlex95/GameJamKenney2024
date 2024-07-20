using Camera;
using UnityEngine;

namespace Core
{
    public class ClickableObjectMoveCamera : MonoBehaviour
    {
        public CameraPositionName cameraPositionName;

        public void OnMouseDown()
        {
            CameraManager.Instance().SetCameraPosition(cameraPositionName);
        }
    }
}