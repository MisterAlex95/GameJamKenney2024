using System.Collections.Generic;
using System.Linq;
using Journal;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        private readonly Dictionary<string, int> _characterState = new();
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #region Global

        public bool CanInteract()
        {
            return !Dialog.DialogManager.Instance.IsDialogActive && !JournalManager.Instance.IsJournalActive;
        }

        #endregion

        #region Character State

        public void SetCharacterState(string characterName, int state)
        {
            _characterState[characterName] = state;
        }

        public int GetCharacterState(string characterName)
        {
            return _characterState.GetValueOrDefault(characterName, -1);
        }

        #endregion

        #region Debug

        [ContextMenu("Reset Character State")]
        public void ResetCharacterState()
        {
            foreach (var character in _characterState.Keys.ToList())
            {
                _characterState[character] = 0;
            }
        }

        [ContextMenu("Incr Character State")]
        public void IncrCharacterState()
        {
            foreach (var character in _characterState.Keys.ToList())
            {
                _characterState[character] += 1;
            }
        }

        #endregion
    }
}