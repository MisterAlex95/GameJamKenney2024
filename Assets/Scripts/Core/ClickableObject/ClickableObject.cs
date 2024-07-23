using System.Collections.Generic;
using Camera;
using UnityEngine;

namespace Core.ClickableObject
{
    [RequireComponent(typeof(BoxCollider))]
    public class ClickableObject : MonoBehaviour
    {
        public List<CameraPositionName> canBeTriggerFromCameraPositions;
        protected bool baseReturned = false;

        private void Start()
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }

        public void OnMouseDown()
        {
            if (!GameManager.Instance.CanInteract())
            {
                baseReturned = true;
                return;
            }

            if (!canBeTriggerFromCameraPositions.Contains(CameraManager.Instance.GetCameraPosition()))
            {
                baseReturned = true;
                return;
            }

            baseReturned = false;
        }
    }
}