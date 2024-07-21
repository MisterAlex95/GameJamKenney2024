using System;
using System.Collections.Generic;
using Core;
using Dialog;
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
        public Button cluesButton;

        [Header("Inventory")] public GameObject inventoryContainer;
        public Button inventoryButton;

        private readonly List<JournalActivityName> _unlockedActivities = new();

        private readonly Dictionary<string, List<JournalActivityName>> _journalActivities =
            new()
            {
                {
                    "A",
                    new List<JournalActivityName>
                    {
                        JournalActivityName.Sleep,
                        JournalActivityName.Sleep,
                        JournalActivityName.Sleep,
                        JournalActivityName.Sleep,
                        JournalActivityName.Eat,
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
            inventoryButton.onClick.AddListener(ToggleInventory);
        }

        private void TogglePlanning()
        {
            if (planningContainer.activeSelf || DialogManager.Instance.IsDialogActive) return;
            planningContainer.SetActive(true);
            cluesContainer.SetActive(false);
            inventoryContainer.SetActive(false);
            mainContainer.SetActive(false);
        }

        private void ToggleClues()
        {
            if (cluesContainer.activeSelf || DialogManager.Instance.IsDialogActive) return;
            cluesContainer.SetActive(true);
            planningContainer.SetActive(false);
            inventoryContainer.SetActive(false);
            mainContainer.SetActive(false);
        }

        private void ToggleInventory()
        {
            if (inventoryContainer.activeSelf || DialogManager.Instance.IsDialogActive) return;
            inventoryContainer.SetActive(true);
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
                inventoryContainer.SetActive(false);
            }
            else
                journalContainer.SetActive(false);
        }


        public void MakeJournalButtonAppear()
        {
            journalButton.SetActive(true);
        }

        public bool IsJournalActive => journalContainer.activeSelf;

        public void UnlockActivity(JournalActivityName activity)
        {
            if (!_unlockedActivities.Contains(activity))
            {
                _unlockedActivities.Add(activity);
            }

            var allCharactersActivities = journalContainer.GetComponentsInChildren<JournalActivity>();

            foreach (var characterActivity in allCharactersActivities)
            {
                var previousValue = characterActivity.GetDropdownValue();
                characterActivity.UpdateDropdownOptions(_unlockedActivities, previousValue);
            }
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
                    Debug.Log("All activities are correct!");
                }
            }
        }
    }
}