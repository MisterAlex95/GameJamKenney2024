using System.Collections.Generic;
using Camera;
using UnityEngine;

namespace Core.ClickableObject
{
    [RequireComponent(typeof(BoxCollider))]
    public class ClickableObject : MonoBehaviour
    {
        public List<CameraPositionName> canBeTriggerFromCameraPositions;
        public int characterState = 0;
        protected bool BaseReturned = false;

        private void Start()
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }

        public void OnMouseDown()
        {
            if (!GameManager.Instance.CanInteract())
            {
                BaseReturned = true;
                return;
            }

            if (GameManager.Instance.GetCurrentState() < characterState)
            {
                BaseReturned = true;
                return;
            }

            if (!canBeTriggerFromCameraPositions.Contains(CameraManager.Instance.GetCameraPosition()))
            {
                BaseReturned = true;
                return;
            }

            BaseReturned = false;
        }
    }
}