using UnityEngine;

namespace Camera
{
    public class ClickToMoveCamera : MonoBehaviour
    {
        public CameraPositionName cameraPositionName;

        public void OnMouseDown()
        {
            CameraManager.Instance().SetCameraPosition(cameraPositionName);
        }
    }
}