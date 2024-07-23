namespace Core.ClickableObject
{
    public class ClickableObjectOpenModalAndTriggerAction : ClickableObject
    {
        public TriggerActionName triggerActionName;
        public string modalText;

        public new void OnMouseDown()
        {
            base.OnMouseDown();
            if (baseReturned) return;

            GameManager.Instance.OpenModal(
                modalText,
                () =>
                {
                    GameManager.Instance.ProcessTriggerAction(triggerActionName);
                    Destroy(gameObject);
                });
        }
    }
}