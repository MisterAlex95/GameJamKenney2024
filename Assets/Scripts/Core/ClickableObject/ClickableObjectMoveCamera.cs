using Camera;

namespace Core.ClickableObject
{
    public class ClickableObjectMoveCamera : ClickableObject
    {
        public CameraPositionName cameraPositionName;

        public new void OnMouseDown()
        {
            base.OnMouseDown();
            if (baseReturned) return;

            if (!canBeTriggerFromCameraPositions.Contains(CameraManager.Instance.GetCameraPosition()))
            {
                baseReturned = true;
                return;
            }

            CameraManager.Instance.SetCameraPosition(cameraPositionName);
        }
    }
}