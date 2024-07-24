using System.Collections.Generic;
using Character;
using Core;
using Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Journal
{
    public class JournalManager : MonoBehaviour
    {
        public GameObject journalContainer;

        [Header("Journal Button")] public GameObject journalButton;

        [Header("Planning")] public GameObject mainContainer;

        [Header("Planning")] public GameObject planningContainer;
        public Button planningButton;

        [Header("Clues")] public GameObject cluesContainer;
        public GameObject dialogCluesContainer;
        public GameObject objectCluesContainer;
        public Button cluesButton;
        public GameObject clueTextPrefab;

        [Header("Letter")] public GameObject letterContainer;
        public Button letterButton;

        [Header("Ticket")] public GameObject ticketContainer;
        public Button ticketButton;
        public GameObject ticketButtonContainer;
        public GameObject ticketCinema;
        public GameObject ticketParking;
        public GameObject ticketRestaurant;

        private bool _hasCheckTheLetter = false;
        private readonly List<JournalActivityName> _unlockedActivities = new();

        private readonly List<string> _cluesAlreadyUnlocked = new();

        private readonly Dictionary<CharacterName, List<JournalActivityName>> _journalActivities =
            new()
            {
                {
                    CharacterName.Daniel,
                    new List<JournalActivityName>
                    {
                        JournalActivityName.Breakfast,
                        JournalActivityName.Cleaning,
                        JournalActivityName.Reading,
                        JournalActivityName.Reading,
                        JournalActivityName.Restaurant,
                    }
                },
                {
                    CharacterName.Ian,
                    new List<JournalActivityName>
                    {
                        JournalActivityName.Sleeping,
                        JournalActivityName.Sleeping,
                        JournalActivityName.Cooking,
                        JournalActivityName.Cooking,
                        JournalActivityName.Lunch,
                    }
                },
                {
                    CharacterName.Livia,
                    new List<JournalActivityName>
                    {
                        JournalActivityName.Breakfast,
                        JournalActivityName.Cinema,
                        JournalActivityName.Cinema,
                        JournalActivityName.Cinema,
                        JournalActivityName.Lunch,
                    }
                }
            };

        public static JournalManager Instance { get; private set; }

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
            planningButton.onClick.AddListener(TogglePlanning);
            cluesButton.onClick.AddListener(ToggleClues);
            letterButton.onClick.AddListener(ToggleLetter);
            ticketButton.onClick.AddListener(ToggleTicket);
            UnlockActivity(JournalActivityName.None);
        }

        private void TogglePlanning()
        {
            if (planningContainer.activeSelf || DialogManager.Instance.IsDialogActive) return;
            planningContainer.SetActive(true);
            cluesContainer.SetActive(false);
            letterContainer.SetActive(false);
            mainContainer.SetActive(false);
            ticketContainer.SetActive(false);
            UpdateActivities();
        }

        private void ToggleClues()
        {
            if (cluesContainer.activeSelf || DialogManager.Instance.IsDialogActive) return;
            cluesContainer.SetActive(true);
            planningContainer.SetActive(false);
            letterContainer.SetActive(false);
            mainContainer.SetActive(false);
            ticketContainer.SetActive(false);
        }

        private void ToggleLetter()
        {
            if (letterContainer.activeSelf || DialogManager.Instance.IsDialogActive) return;
            letterContainer.SetActive(true);
            planningContainer.SetActive(false);
            cluesContainer.SetActive(false);
            mainContainer.SetActive(false);
            ticketContainer.SetActive(false);

            _hasCheckTheLetter = true;

            // First lecture we unlocked a first clue
            if (_hasCheckTheLetter)
            {
                AddObjectClue(
                    "- Maddy Thomas was killed between 8am and 1pm.");
            }
        }

        private void ToggleTicket()
        {
            if (ticketContainer.activeSelf || DialogManager.Instance.IsDialogActive) return;
            ticketContainer.SetActive(true);
            letterContainer.SetActive(false);
            planningContainer.SetActive(false);
            cluesContainer.SetActive(false);
            mainContainer.SetActive(false);
        }

        public void ToggleJournal()
        {
            if (!journalContainer.activeSelf && GameManager.Instance.CanInteract())
            {
                journalContainer.SetActive(true);
                mainContainer.SetActive(true);
                planningContainer.SetActive(false);
                cluesContainer.SetActive(false);
                letterContainer.SetActive(false);
                ticketContainer.SetActive(false);
            }
            else
            {
                journalContainer.SetActive(false);

                // First time it opens the letter and close the journal
                if (_hasCheckTheLetter)
                {
                    GameManager.Instance.ProcessTriggerAction(TriggerActionName.Enable_Foot_Print);
                }
            }
        }

        public void MakeJournalButtonAppear()
        {
            journalButton.SetActive(true);
        }

        public void MakeTicketParkingAppear()
        {
            ticketParking.SetActive(true);
        }

        public void MakeTicketCinemaAppear()
        {
            ticketCinema.SetActive(true);
        }


        public void MakeTicketButtonAppear()
        {
            ticketButtonContainer.SetActive(true);
        }

        public bool IsJournalActive => journalContainer.activeSelf;

        public void UnlockActivity(JournalActivityName activity)
        {
            if (_unlockedActivities.Contains(activity))
            {
                return;
            }

            _unlockedActivities.Add(activity);
            var allCharactersActivities = journalContainer.GetComponentsInChildren<JournalActivity>();

            foreach (var characterActivity in allCharactersActivities)
            {
                var previousValue = characterActivity.GetDropdownValue();
                characterActivity.UpdateDropdownOptions(_unlockedActivities, previousValue);
            }
        }

        private void UpdateActivities()
        {
            var allCharactersActivities = journalContainer.GetComponentsInChildren<JournalActivity>();

            foreach (var characterActivity in allCharactersActivities)
            {
                var previousValue = characterActivity.GetDropdownValue();
                characterActivity.UpdateDropdownOptions(_unlockedActivities, previousValue);
            }
        }

        public void AddDialogClue(string clue)
        {
            if (_cluesAlreadyUnlocked.Contains(clue)) return;
            _cluesAlreadyUnlocked.Add(clue);

            var dialogClue = Instantiate(clueTextPrefab,
                dialogCluesContainer.transform);
            dialogClue.GetComponentInChildren<TMP_Text>().text = clue;
        }

        public void AddObjectClue(string clue)
        {
            if (_cluesAlreadyUnlocked.Contains(clue)) return;
            _cluesAlreadyUnlocked.Add(clue);

            var dialogClue = Instantiate(clueTextPrefab,
                objectCluesContainer.transform);
            dialogClue.GetComponentInChildren<TMP_Text>().text = clue;
        }

        public void CheckJournal()
        {
            var allCharactersActivities = journalContainer.GetComponentsInChildren<JournalActivity>();
            var checkActivities = true;

            foreach (var characterActivity in allCharactersActivities)
            {
                var characterName = characterActivity.characterName;
                var activityName = (JournalActivityName)characterActivity.GetDropdownValue();
                var activityHour = characterActivity.activityHour;

                if (_journalActivities.TryGetValue(characterName, out var activities))
                {
                    if (activities[activityHour] != activityName)
                    {
                        checkActivities = false;
                    }
                }

                if (checkActivities)
                {
                    GameManager.Instance.ProcessTriggerAction(TriggerActionName.Planning_Correct);
                }
            }
        }
    }
}