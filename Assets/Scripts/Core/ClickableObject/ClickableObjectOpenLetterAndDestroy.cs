using Localization;

namespace Core.ClickableObject
{
    public class ClickableObjectOpenModalAndTriggerAction : ClickableObject
    {
        public TriggerActionName triggerActionName;
        public string modalText;
        public int modalLocId;

        public new void OnMouseDown()
        {
            base.OnMouseDown();
            if (BaseReturned) return;

            GameManager.Instance.OpenModal(
                LocalizationManager.Instance.GetLocalizedValue(modalLocId),
                () =>
                {
                    GameManager.Instance.ProcessTriggerAction(triggerActionName);
                    Destroy(gameObject);
                });
        }
    }
}