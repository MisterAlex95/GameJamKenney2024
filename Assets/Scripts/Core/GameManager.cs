using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character;
using Dialog;
using Journal;
using Sound;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Introduction")] public GameObject blackScreen;
        public GameObject letterScreen;
        private Image _blackScreenImage;
        private TMP_Text _blackScreenText;

        private readonly Dictionary<CharacterName, int> _characterState = new();

        // Modal Variables
        [Header("Modal")] public GameObject modalScreen;
        private bool _isModalActive;
        private TMP_Text _modalText;
        private Action _modalCloseAction;

        // Letter Variables
        [Header("Letter")] public GameObject letterToSpawn;
        public Transform positionToSpawnLetter;
        private bool _isPrintingFinished;
        private string _textLetter;

        private CharacterData _characterData;
        private int _currentState = 0;

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
            _modalText = modalScreen.GetComponentInChildren<TMP_Text>();
            _blackScreenImage = blackScreen.GetComponent<Image>();
            _blackScreenText = blackScreen.GetComponentInChildren<TMP_Text>();
            SetCharacterState(CharacterName.Camden, _currentState);

            // StartCoroutine(Introduction());
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

        public void OpenLetter(Action action)
        {
            StartCoroutine(OpenLetterCoroutine(action));
        }

        private IEnumerator OpenLetterCoroutine(Action action)
        {
            var textLetterComponent = letterScreen.GetComponentInChildren<TMP_Text>();
            _isPrintingFinished = false;
            _textLetter = textLetterComponent.text;
            textLetterComponent.text = "";

            letterScreen.SetActive(true);
            yield return new WaitForSeconds(1);

            foreach (var t in _textLetter)
            {
                textLetterComponent.text += t;
                yield return new WaitForSeconds(0.01f);
            }

            _isPrintingFinished = true;
            var letterButton = letterScreen.GetComponent<Button>();
            letterButton.onClick.AddListener(() => action?.Invoke());
        }

        public bool IsPrintingFinished()
        {
            return _isPrintingFinished;
        }

        #endregion

        #region Modal

        public void OpenModal(string text, Action onClose = default)
        {
            _modalText.text = text;
            modalScreen.SetActive(true);
            _isModalActive = true;
            _modalCloseAction = onClose;
        }

        public void CloseModal()
        {
            modalScreen.SetActive(false);
            _modalText.text = "";
            _isModalActive = false;
            _modalCloseAction?.Invoke();
        }

        public bool IsModalActive()
        {
            return _isModalActive;
        }

        #endregion

        #region Global

        public bool CanInteract()
        {
            return !DialogManager.Instance.IsDialogActive && !JournalManager.Instance.IsJournalActive;
        }

        public void SetMoveDisabled(bool isDisabled)
        {
            var arrowsMove = FindObjectsByType<ArrowMove>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var arrowMove in arrowsMove)
            {
                arrowMove.gameObject.SetActive(!isDisabled);
            }
        }

        public void ProcessTriggerAction(TriggerActionName triggerAction)
        {
            switch (triggerAction)
            {
                case TriggerActionName.None:
                    break;
                case TriggerActionName.Letter_Appear:
                    Instantiate(letterToSpawn, positionToSpawnLetter);
                    break;
                case TriggerActionName.Enable_Notebook:
                    JournalManager.Instance.MakeJournalButtonAppear();
                    break;
                case TriggerActionName.Tickets_Appear:
                    JournalManager.Instance.MakeTicketButtonAppear();
                    break;
                case TriggerActionName.Enable_Foot_Print:
                    if (GetCharacterState(CharacterName.Camden) == 0)
                    {
                        SetCharacterState(CharacterName.Camden, 1);
                    }

                    break;
                case TriggerActionName.Enable_Movements:
                    SetMoveDisabled(false);
                    break;
                case TriggerActionName.Add_Foot_Print_To_Clues:
                    JournalManager.Instance.AddObjectClue("- The floor is still a bit dirty despite the heavy clean.");
                    break;
                case TriggerActionName.Add_Fridge_To_Clues:
                    JournalManager.Instance.AddObjectClue("- A lot of fresh food was cooked recently.");
                    break;
                case TriggerActionName.Add_Diner_To_Clues:
                    JournalManager.Instance.AddObjectClue("- Only two people ate here for lunch.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(triggerAction), triggerAction, null);
            }
        }

        #endregion

        #region Character State

        private int GetCurrentState()
        {
            var newState = GameManager.Instance.GetCharacterState(_characterData.characterName);
            if (newState == _currentState) return _currentState;

            _currentState = newState;
            return _currentState;
        }

        public void SetCharacterState(CharacterName characterName, int state)
        {
            _characterState[characterName] = state;
        }

        public void IncrCharacterState(CharacterName characterName)
        {
            _characterState[characterName] += 1;
        }

        public int GetCharacterState(CharacterName characterName)
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

        [ContextMenu("Print Character State")]
        public void PrintCharacterState()
        {
            foreach (var character in _characterState.Keys.ToList())
            {
                Debug.Log($"{character}: {_characterState[character]}");
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