using Character;
using Core;
using Localization;
using UnityEngine;

namespace Dialog
{
    public class Dialog : IDialog
    {
        private int _currentDialogIndex;
        private readonly string[] _texts;
        private readonly int[] _locIds;
        private readonly Sprite _speakerSprite;
        private readonly string _speakerName;
        private readonly bool _increaseStageOnDialogEnd;
        private readonly TriggerActionName _triggerActionName;
        private readonly CharacterName _characterNameToIncreaseStage;

        public Dialog(DialogData dialogData)
        {
            _triggerActionName = dialogData.triggerActionName;
            _increaseStageOnDialogEnd = dialogData.increaseStageOnDialogEnd;
            _characterNameToIncreaseStage = dialogData.characterToIncreaseStage;
            _speakerSprite = dialogData.speakerSprite;
            _speakerName = dialogData.speakerName;
            _texts = dialogData.dialogText.ToArray();
            _locIds = dialogData.dialogLocId.ToArray();
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
            return _currentDialogIndex < _locIds.Length;
        }

        public bool ShouldIncreaseStageOnDialogEnd()
        {
            return _increaseStageOnDialogEnd;
        }

        public CharacterName GetCharacterToIncreaseStage()
        {
            return _characterNameToIncreaseStage;
        }

        public TriggerActionName GetTriggerActionName()
        {
            return _triggerActionName;
        }

        public string GetNextDialog()
        {
            if (!HasNextDialog()) return null;
            var dialog = LocalizationManager.Instance.GetLocalizedValue(_locIds[_currentDialogIndex]);
            _currentDialogIndex++;

            return dialog;
        }
    }
}