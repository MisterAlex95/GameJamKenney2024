using UnityEngine;

namespace Camera
{
    [CreateAssetMenu(fileName = "CameraPosition", menuName = "Camera/CameraPosition")]
    public class CameraPosition : ScriptableObject
    {
        public Transform transform;
        public CameraPositionName cameraPositionName;
    }
}