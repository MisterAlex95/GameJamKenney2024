using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    [CreateAssetMenu(fileName = "DialogData", menuName = "Dialog/DialogData")]
    public class DialogData : ScriptableObject
    {
        public bool looping = false;
        public int enableDialogAtStage = 0;
        public Sprite speakerSprite;
        public string speakerName;
        public List<string> dialogText = new();
    }
}