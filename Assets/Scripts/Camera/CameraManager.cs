using UnityEngine;

namespace Camera
{
    public class CameraManager : MonoBehaviour
    {
        public CameraPosition[] cameraPositions;
        public CameraPositionName currentRoom;
        public CameraPositionName defaultRoom = CameraPositionName.Hall;

        private static CameraManager _instance;

        public static CameraManager Instance()
        {
            return _instance;
        }

        public CameraPositionName GetCameraPosition()
        {
            return currentRoom;
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void Start()
        {
            SetCameraPosition(defaultRoom);
        }

        public void SetCameraPosition(CameraPositionName roomName)
        {
            foreach (var cameraPosition in cameraPositions)
            {
                if (cameraPosition.cameraPositionName != roomName) continue;

                if (UnityEngine.Camera.main != null)
                {
                    UnityEngine.Camera.main.transform.SetPositionAndRotation(cameraPosition.transform.position,
                        cameraPosition.transform.rotation);
                }

                currentRoom = roomName;
                return;
            }

            Debug.LogError("Camera position not found");
        }
    }
}