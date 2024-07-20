using System.Collections.Generic;
using Character;
using Core;
using UnityEngine;

namespace Dialog
{
    [CreateAssetMenu(fileName = "DialogData", menuName = "Dialog/DialogData")]
    public class DialogData : ScriptableObject
    {
        public bool looping = false;
        public int enableDialogAtStage = 0;

        public bool increaseStageOnDialogEnd = false;
        public CharacterName characterToIncreaseStage;

        public TriggerActionName triggerActionName = TriggerActionName.None;

        public Sprite speakerSprite;
        public string speakerName;

        public List<string> dialogText = new();
    }
}