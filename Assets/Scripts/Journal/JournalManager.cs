using System.Collections;
using System.Collections.Generic;
using Character;
using Core;
using Dialog;
using Localization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Journal
{
    public class JournalManager : MonoBehaviour
    {
        public GameObject journalContainer;

        [Header("Journal Button")] public GameObject journalButton;

        [Header("Main Container")] public GameObject mainContainer;

        [Header("Planning")] public GameObject planningContainer;
        public GameObject activityContainer;
        public GameObject activityModal;
        public GameObject activityModalButtonPrefab;
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
        private readonly List<int> _cluesAlreadyUnlocked = new();

        private readonly Dictionary<CharacterName, Dictionary<int, JournalActivityName>> _journalActivities = new()
        {
            {
                CharacterName.Daniel,
                new Dictionary<int, JournalActivityName>
                {
                    {9, JournalActivityName.None},
                    {10, JournalActivityName.None},
                    {11, JournalActivityName.None},
                    {12, JournalActivityName.None},
                    {13, JournalActivityName.None}
                }
            },
            {
                CharacterName.Ian,
                new Dictionary<int, JournalActivityName>
                {
                    {9, JournalActivityName.None},
                    {10, JournalActivityName.None},
                    {11, JournalActivityName.None},
                    {12, JournalActivityName.None},
                    {13, JournalActivityName.None}
                }
            },
            {
                CharacterName.Livia,
                new Dictionary<int, JournalActivityName>
                {
                    {9, JournalActivityName.None},
                    {10, JournalActivityName.None},
                    {11, JournalActivityName.None},
                    {12, JournalActivityName.None},
                    {13, JournalActivityName.None}
                }
            }
        };


        private readonly Dictionary<CharacterName, Dictionary<int, JournalActivityName>> _journalActivitiesSoluce =
            new()
            {
                {
                    CharacterName.Daniel,
                    new Dictionary<int, JournalActivityName>
                    {
                        {9, JournalActivityName.Breakfast},
                        {10, JournalActivityName.Cleaning},
                        {11, JournalActivityName.Reading},
                        {12, JournalActivityName.Reading},
                        {13, JournalActivityName.Restaurant}
                    }
                },
                {
                    CharacterName.Ian,
                    new Dictionary<int, JournalActivityName>
                    {
                        {9, JournalActivityName.Sleeping},
                        {10, JournalActivityName.Sleeping},
                        {11, JournalActivityName.Cooking},
                        {12, JournalActivityName.Cooking},
                        {13, JournalActivityName.Lunch}
                    }
                },
                {
                    CharacterName.Livia,
                    new Dictionary<int, JournalActivityName>
                    {
                        {9, JournalActivityName.Breakfast},
                        {10, JournalActivityName.Cinema},
                        {11, JournalActivityName.Cinema},
                        {12, JournalActivityName.Cinema},
                        {13, JournalActivityName.Lunch}
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
            RenameAllButtons();
        }

        private void TogglePlanning()
        {
            if (planningContainer.activeSelf || DialogManager.Instance.IsDialogActive) return;
            planningContainer.SetActive(true);
            cluesContainer.SetActive(false);
            letterContainer.SetActive(false);
            mainContainer.SetActive(false);
            ticketContainer.SetActive(false);
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
                AddObjectClue(41);
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

                // First time it sees the restaurant ticket and close the journal
                if (_hasCheckTheLetter)
                {
                    GameManager.Instance.ProcessTriggerAction(TriggerActionName.Checked_Restaurant_Ticket);
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

        public void MakeTicketRestaurantAppear()
        {
            ticketRestaurant.SetActive(true);
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

            // Spawn the button in the activity modal
            var activityButton = Instantiate(activityModalButtonPrefab, activityContainer.transform);
            activityButton.GetComponent<ActivityModalButton>().SetActivity(activity);
            activityButton.GetComponentInChildren<TMP_Text>().text =
                LocalizationManager.Instance.GetLocalizedValue(activity.ToFriendlyString());
        }

        public void AddDialogClue(int locId)
        {
            if (_cluesAlreadyUnlocked.Contains(locId)) return;
            _cluesAlreadyUnlocked.Add(locId);

            var dialogClue = Instantiate(clueTextPrefab,
                dialogCluesContainer.transform);
            dialogClue.GetComponentInChildren<TMP_Text>().text = LocalizationManager.Instance.GetLocalizedValue(locId);
            ;
        }

        public void AddObjectClue(int locId)
        {
            if (_cluesAlreadyUnlocked.Contains(locId)) return;
            _cluesAlreadyUnlocked.Add(locId);

            var dialogClue = Instantiate(clueTextPrefab,
                objectCluesContainer.transform);
            dialogClue.GetComponentInChildren<TMP_Text>().text = LocalizationManager.Instance.GetLocalizedValue(locId);
        }

        private void CheckJournal()
        {
            foreach (var solutionCharacter in _journalActivitiesSoluce.Keys)
            {
                var solutionActivities = _journalActivitiesSoluce[solutionCharacter];
                var playerActivities = _journalActivities[solutionCharacter];

                for (var i = 9; i < (9 + solutionActivities.Count); i++)
                {
                    if (solutionActivities[i] != playerActivities[i])
                    {
                        return;
                    }
                }
            }

            GameManager.Instance.ProcessTriggerAction(TriggerActionName.Planning_Correct);
        }

        public void ToggleActivityModal(CharacterName characterName, int activityHour)
        {
            activityModal.SetActive(!activityModal.activeSelf);

            if (!activityModal.activeSelf) return;
            foreach (var activityModalButton in GameObject.FindObjectsByType<ActivityModalButton>(
                         FindObjectsInactive.Include,
                         FindObjectsSortMode.None))
            {
                activityModalButton.SetOnClick((activity) =>
                {
                    _journalActivities[characterName][activityHour] = activity;
                    RenameButton(characterName, activityHour,
                        LocalizationManager.Instance.GetLocalizedValue(activity.ToFriendlyString()));
                    activityModal.SetActive(false);
                    StartCoroutine(DelayedCheckJournal());
                });
            }
        }

        private void RenameButton(CharacterName characterName, int activityHour, string name)
        {
            var buttons = GameObject.FindObjectsByType<JournalActivity>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None);

            foreach (var button in buttons)
            {
                if (button.characterName == characterName && button.activityHour == activityHour)
                {
                    button.GetComponentInChildren<TMP_Text>().text = name;
                }
            }
        }

        private void RenameAllButtons()
        {
            var buttons = GameObject.FindObjectsByType<JournalActivity>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None);

            foreach (var button in buttons)
            {
                button.GetComponentInChildren<TMP_Text>().text =
                    LocalizationManager.Instance.GetLocalizedValue(JournalActivityName.None.ToFriendlyString());
            }
        }

        private IEnumerator DelayedCheckJournal()
        {
            yield return new WaitForSeconds(1f);
            CheckJournal();
        }
    }
}