using Character;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dialog
{
    public class DialogBox : MonoBehaviour, IPointerClickHandler
    {
        private TMP_Text _dialogText;
        private Character.Character _character;

        private void Awake()
        {
            _dialogText = GetComponentInChildren<TMP_Text>();
        }

        public void SetDialog(Character.Character character)
        {
            _character = character;
        }

        public void GetNextDialog()
        {
            if (_character.HasNextDialog())
            {
                _dialogText.text = _character.GetNextDialog();
            }
            else
            {
                DialogManager.Instance.EndDialog();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GetNextDialog();
        }
    }
}