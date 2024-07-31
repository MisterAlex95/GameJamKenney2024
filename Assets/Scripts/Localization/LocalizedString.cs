using TMPro;
using UnityEngine;

namespace Localization
{
    public class LocalizedString : MonoBehaviour
    {
        public int key;

        private void Start()
        {
            GetComponent<TMP_Text>().text = LocalizationManager.Instance.GetLocalizedValue(key);
        }

        public void UpdateText()
        {
            GetComponent<TMP_Text>().text = LocalizationManager.Instance.GetLocalizedValue(key);
        }
    }
}