using System;
using UnityEngine;
using UnityEngine.UI;

namespace Journal
{
    public class ActivityModalButton : MonoBehaviour
    {
        private JournalActivityName _activityName;
        private Action<JournalActivityName> _onClick;

        public void SetActivity(JournalActivityName activityName)
        {
            _activityName = activityName;
        }

        public void SetOnClick(Action<JournalActivityName> onClick)
        {
            _onClick = onClick;
            var button = GetComponent<Button>();
            button.onClick.AddListener(() => _onClick?.Invoke(_activityName));
        }
    }
}