using UnityEngine;

namespace Core
{
    public class ClickableObjectRunAnimation : MonoBehaviour
    {
        public string triggerName;
        public bool state = false;

        public void OnMouseDown()
        {
            if (triggerName == null) return;
            state = !state;
            GetComponent<Animator>().SetBool(triggerName, state);
        }
    }
}