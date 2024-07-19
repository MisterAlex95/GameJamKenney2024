using UnityEngine;

namespace Letter
{
    public class PrintingEffect : MonoBehaviour
    {
        public float speed = 0.1f;
        private string _text;
        private TMPro.TextMeshProUGUI _textComponent;

        private void Start()
        {
            _textComponent = GetComponent<TMPro.TextMeshProUGUI>();
            _text = _textComponent.text;
            _textComponent.text = "";
            StartCoroutine(PrintText());
        }

        private System.Collections.IEnumerator PrintText()
        {
            foreach (var t in _text)
            {
                _textComponent.text += t;

                yield return new WaitForSeconds(speed);
            }
        }
    }
}