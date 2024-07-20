using UnityEngine;

namespace Dialog
{
    public class Dialog : IDialog
    {
        private int _currentDialogIndex;
        private readonly string[] _texts;
        private readonly Sprite _speakerSprite;
        private readonly string _speakerName;

        public Dialog(DialogData dialogData)
        {
            _speakerSprite = dialogData.speakerSprite;
            _speakerName = dialogData.speakerName;
            _texts = dialogData.dialogText.ToArray();
        }

        public Sprite GetSpeakerSprite()
        {
            return _speakerSprite;
        }

        public string GetSpeakerName()
        {
            return _speakerName;
        }

        public bool HasNextDialog()
        {
            return _currentDialogIndex < _texts.Length;
        }

        public string GetNextDialog()
        {
            if (!HasNextDialog()) return null;
            var dialog = _texts[_currentDialogIndex];
            _currentDialogIndex++;

            return dialog;
        }
    }
}