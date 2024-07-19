using UnityEngine;
using UnityEngine.EventSystems;

namespace Letter
{
    public class LetterManager : MonoBehaviour, IPointerClickHandler
    {
        public float speed = 0.1f;
        private string _text;
        private bool _isPrintingFinished;
        public TMPro.TextMeshProUGUI textLetterComponent;
        public TMPro.TextMeshProUGUI endMessage;

        private void Start()
        {
            endMessage.color = new Color(1, 1, 1, 0);
            _isPrintingFinished = false;
            _text = textLetterComponent.text;
            textLetterComponent.text = "";
            StartCoroutine(PrintText());
        }

        private System.Collections.IEnumerator PrintText()
        {
            foreach (var t in _text)
            {
                textLetterComponent.text += t;

                yield return new WaitForSeconds(speed);
            }

            _isPrintingFinished = true;
        }

        private void Update()
        {
            if (!_isPrintingFinished) return;
            var color = endMessage.color;
            color.a = Mathf.PingPong(Time.time, 1);
            endMessage.color = color;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isPrintingFinished)
            {
                Debug.Log("Next scene");
            }
        }
    }
}