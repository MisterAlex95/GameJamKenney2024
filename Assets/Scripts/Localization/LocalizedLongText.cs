using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Localization
{
    public class LocalizedLongText : MonoBehaviour
    {
        [TextArea(15, 20)] [SerializeField] public List<string> texts = new(4);

        private void Start()
        {
            GetComponent<TMP_Text>().text = texts[(int) LocalizationManager.Instance.GetCurrentLanguage()];
        }

        public void UpdateText()
        {
            GetComponent<TMP_Text>().text = texts[(int) LocalizationManager.Instance.GetCurrentLanguage()];
        }
    }
}