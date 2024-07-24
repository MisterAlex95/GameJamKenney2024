using Camera;

namespace Core.ClickableObject
{
    public class ClickableObjectMoveCamera : ClickableObject
    {
        public CameraPositionName cameraPositionName;

        public new void OnMouseDown()
        {
            base.OnMouseDown();
            if (BaseReturned) return;

            if (!canBeTriggerFromCameraPositions.Contains(CameraManager.Instance.GetCameraPosition()))
            {
                BaseReturned = true;
                return;
            }

            CameraManager.Instance.SetCameraPosition(cameraPositionName);
        }
    }
}