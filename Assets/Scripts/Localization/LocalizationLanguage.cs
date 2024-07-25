namespace Localization
{
    public enum LocalizationLanguage
    {
        English,
        French
    }

    public static class LocalizationLanguageExtensions
    {
        public static string ToLanguageCode(this LocalizationLanguage language)
        {
            return language switch
            {
                LocalizationLanguage.English => "en",
                LocalizationLanguage.French => "fr",
                _ => "en"
            };
        }
    }
}