using UnityEngine;

namespace Core.ClickableObject
{
    public class ClickableObjectRunAnimation : ClickableObject
    {
        public string triggerName;
        public bool state = false;

        public new void OnMouseDown()
        {
            base.OnMouseDown();
            if (baseReturned || triggerName == null)
            {
                baseReturned = true;
                return;
            }

            state = !state;
            GetComponent<Animator>().SetBool(triggerName, state);
        }
    }
}