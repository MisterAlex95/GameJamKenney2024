using System;
using System.Collections.Generic;
using UnityEngine;

namespace Journal
{
    public class JournalManager : MonoBehaviour
    {
        public GameObject journalContainer;
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

        // Singleton
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
            ToggleJournal();
        }

        public void ToggleJournal()
        {
            journalContainer.SetActive(!journalContainer.activeSelf);
        }

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