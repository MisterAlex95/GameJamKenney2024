using System.Collections.Generic;
using System.Linq;
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

        public void SetCharacterState(string characterName, int state)
        {
            _characterState[characterName] = state;
        }

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

        public int GetCharacterState(string characterName)
        {
            return _characterState.GetValueOrDefault(characterName, -1);
        }
    }
}