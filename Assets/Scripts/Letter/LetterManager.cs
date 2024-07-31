using System.Collections;
using EasyTransition;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Letter
{
    public class LetterManager : MonoBehaviour, IPointerClickHandler
    {
        public TransitionSettings transitionSettings;
        private TransitionManager _transitionManager;

        public float speed = 0.01f;
        private string _text;
        private bool _isPrintingFinished;
        public TMPro.TextMeshProUGUI textLetterComponent;
        public TMPro.TextMeshProUGUI endMessage;

        public Button clickableZone;
        public GameObject newMailContainer;
        public GameObject mailContainer;

        private void Start()
        {
            endMessage.color = new Color(1, 1, 1, 0);
            textLetterComponent.color = new Color(1, 1, 1, 0);
            _isPrintingFinished = false;

            _transitionManager = TransitionManager.Instance();

            clickableZone.onClick.AddListener(() =>
            {
                Destroy(newMailContainer);
                mailContainer.SetActive(true);
                StartCoroutine(PrintText());
            });
        }

        private IEnumerator PrintText()
        {
            yield return new WaitForSeconds(1);
            _text = textLetterComponent.text;
            textLetterComponent.text = "";
            textLetterComponent.color = Color.black;
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
            if (!_isPrintingFinished) return;
            _transitionManager.Transition("GameScene", transitionSettings, 0);
        }
    }
}