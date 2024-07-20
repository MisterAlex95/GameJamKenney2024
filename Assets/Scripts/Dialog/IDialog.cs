using Character;
using Core;
using UnityEngine;

namespace Dialog
{
    public interface IDialog
    {
        string GetNextDialog();
        Sprite GetSpeakerSprite();
        string GetSpeakerName();
        bool HasNextDialog();
        bool ShouldIncreaseStageOnDialogEnd();
        CharacterName GetCharacterToIncreaseStage();
        TriggerActionName GetTriggerActionName();
    }
}