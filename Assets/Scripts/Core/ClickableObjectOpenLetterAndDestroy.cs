using System.Collections;
using System.Collections.Generic;
using Camera;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(BoxCollider))]
    public class ClickableObjectOpenLetterAndDestroy : MonoBehaviour
    {
        public List<CameraPositionName> canBeTriggerFromCameraPositions;

        private void Start()
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }

        public void OnMouseDown()
        {
            if (!GameManager.Instance.CanInteract()) return;
            if (!canBeTriggerFromCameraPositions.Contains(CameraManager.Instance().GetCameraPosition())) return;

            GameManager.Instance.OpenModal(
                "You successfully took an object. The letter is now available in your inventory. Simply tap on your Notebook to open or close it or to access other  clues you already found or objects.",
                () => { Destroy(gameObject); });
        }
    }
}