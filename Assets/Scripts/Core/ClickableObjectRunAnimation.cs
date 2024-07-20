using System.Collections.Generic;
using Camera;
using UnityEngine;

namespace Core
{
    public class ClickableObjectRunAnimation : MonoBehaviour
    {
        public List<CameraPositionName> canBeTriggerFromCameraPositions;
        public string triggerName;
        public bool state = false;

        public void OnMouseDown()
        {
            if (triggerName == null) return;
            if (!canBeTriggerFromCameraPositions.Contains(CameraManager.Instance().GetCameraPosition())) return;
            state = !state;
            GetComponent<Animator>().SetBool(triggerName, state);
        }
    }
}