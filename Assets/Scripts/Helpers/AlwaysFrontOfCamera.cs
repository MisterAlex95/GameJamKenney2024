using UnityEngine;

namespace Helpers
{
    public class AlwaysFrontOfCamera : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            // Make the object always face the camera
            transform.LookAt(_camera.transform);
        }
    }
}