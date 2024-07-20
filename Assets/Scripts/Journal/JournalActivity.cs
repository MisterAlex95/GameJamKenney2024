using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Journal
{
    public class JournalActivity : MonoBehaviour
    {
        private TMP_Dropdown _dropdown;
        public string characterName;
        public int activityHour;

        private void Start()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            _dropdown.ClearOptions();
            _dropdown.AddOptions(GenerateDropdownOptions(new List<JournalActivityName>() { JournalActivityName.None }));
            _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        private List<TMP_Dropdown.OptionData> GenerateDropdownOptions(List<JournalActivityName> options)
        {
            var dropdownOptions = new List<TMP_Dropdown.OptionData>();
            foreach (var option in options)
            {
                dropdownOptions.Add(new TMP_Dropdown.OptionData(option.ToFriendlyString()));
            }

            return dropdownOptions;
        }

        public void UpdateDropdownOptions(List<JournalActivityName> options, int previousValue)
        {
            _dropdown.ClearOptions();
            _dropdown.AddOptions(GenerateDropdownOptions(options));
            _dropdown.value = previousValue;
        }

        private void OnDropdownValueChanged(int value)
        {
            JournalManager.Instance.CheckJournal();
        }

        public int GetDropdownValue()
        {
            return _dropdown?.value ?? 0;
        }
    }
}