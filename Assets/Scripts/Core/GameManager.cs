using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Journal;
using Sound;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public GameObject blackScreen;
        private Image _blackScreenImage;
        private TMP_Text _blackScreenText;

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

        private void Start()
        {
            SetMoveDisabled(true);
            _blackScreenImage = blackScreen.GetComponent<Image>();
            _blackScreenText = blackScreen.GetComponentInChildren<TMP_Text>();
            StartCoroutine(Introduction());
        }

        #region Transitions

        private IEnumerator Introduction()
        {
            _blackScreenText.color = new Color(1, 1, 1, 0);
            _blackScreenText.text = "Later the same day...";

            blackScreen.SetActive(true);

            // BlackScreen for 1sec
            yield return new WaitForSeconds(1f);
            float time = 0;

            // Display Message
            while (time < 1f)
            {
                time += Time.deltaTime;
                _blackScreenText.color = new Color(1, 1, 1, 0 + time);
                yield return null;
            }

            time = 0;
            // Remove Message
            while (time < 2f)
            {
                time += Time.deltaTime;
                _blackScreenText.color = new Color(1, 1, 1, 1 - time / 2f);
                yield return null;
            }

            // Bell Door
            SoundManager.Instance.PlaySound("belldoor");
            yield return new WaitWhile(SoundManager.Instance.IsPlaying);

            // Walk to the door
            SoundManager.Instance.PlaySound("walking");
            yield return new WaitWhile(SoundManager.Instance.IsPlaying);

            // Door
            SoundManager.Instance.PlaySound("opendoor");
            yield return new WaitWhile(SoundManager.Instance.IsPlaying);

            time = 0;
            while (time < 3f)
            {
                time += Time.deltaTime;
                _blackScreenImage.color = new Color(0, 0, 0, 1 - time / 2f);
                yield return null;
            }

            blackScreen.SetActive(false);
        }

        #endregion

        #region Global

        public bool CanInteract()
        {
            return !Dialog.DialogManager.Instance.IsDialogActive && !JournalManager.Instance.IsJournalActive;
        }

        public void SetMoveDisabled(bool isDisabled)
        {
            foreach (var arrowMove in FindObjectsOfType<ArrowMove>())
            {
                arrowMove.gameObject.SetActive(!isDisabled);
            }
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