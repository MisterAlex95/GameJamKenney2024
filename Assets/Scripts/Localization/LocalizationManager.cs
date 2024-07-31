using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager Instance;

        private Dictionary<int, string> _localizedText;
        private LocalizationLanguage _currentLanguage = LocalizationLanguage.English;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                LoadLocalizedText(LocalizationLanguage.English.ToLanguageCode());
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void LoadLocalizedText(string language)
        {
            _localizedText = new Dictionary<int, string>();
            var fileName = $"localization_{language}.csv";
            var filePath = Path.Combine(Application.streamingAssetsPath, fileName);

            if (File.Exists(filePath))
            {
                var data = File.ReadAllLines(filePath);

                for (var i = 1; i < data.Length; i++)
                {
                    // Escape comments
                    if (data[i].StartsWith("//")) continue;

                    var row = data[i].Split(',');
                    var key = int.Parse(row[0]);
                    // join all the other elements in the row in case the value contains commas
                    var value = string.Join(",", row, 1, row.Length - 1);
                    _localizedText[key] = value;
                }

                Debug.Log("Localization data loaded for language: " + language);
            }
            else
            {
                Debug.LogError("Cannot find file: " + filePath);
            }
        }

        public string GetLocalizedValue(int key)
        {
            return _localizedText.TryGetValue(key, out var value) ? value : key.ToString();
        }

        public void SetLanguage(LocalizationLanguage language)
        {
            if (_currentLanguage == language) return;
            _currentLanguage = language;
            LoadLocalizedText(language.ToLanguageCode());

            // Update all localized strings
            var localizedStrings = Object.FindObjectsByType<LocalizedString>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

            foreach (var localizedString in localizedStrings)
            {
                localizedString.UpdateText();
            }
        }
    }
}