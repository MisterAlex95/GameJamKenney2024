using Character;
using UnityEngine;
using UnityEngine.UI;

namespace Journal
{
    public class JournalActivity : MonoBehaviour
    {
        public CharacterName characterName;
        public int activityHour;

        private void Start()
        {
            var button = GetComponentInChildren<Button>();
            button.onClick.AddListener((() =>
                JournalManager.Instance.ToggleActivityModal(characterName, activityHour)));
        }
    }
}