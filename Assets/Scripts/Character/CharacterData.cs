using Core;
using Dialog;
using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Character/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public string characterName;
        public DialogData[] dialogs;
    }
}