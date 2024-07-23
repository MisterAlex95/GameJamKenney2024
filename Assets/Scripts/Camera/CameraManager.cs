using Core;
using UnityEngine;

namespace Camera
{
    public class CameraManager : MonoBehaviour
    {
        public CameraPosition[] cameraPositions;
        public CameraPositionName currentRoom;
        public CameraPositionName defaultRoom = CameraPositionName.Hall;

        public static CameraManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
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

        public CameraPositionName GetCameraPosition()
        {
            return currentRoom;
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
                GameManager.Instance.UpdateArrowMoveRegardingPosition();
                return;
            }

            Debug.LogError("Camera position not found");
        }
    }
}