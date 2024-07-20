using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    [CreateAssetMenu(fileName = "DialogData", menuName = "Dialog/DialogData")]
    public class DialogData : ScriptableObject
    {
        public bool looping = false;
        public int enableDialogAtStage = 0;
        public List<string> dialogText = new();
    }
}