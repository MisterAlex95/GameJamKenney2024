namespace Localization
{
    public enum LocalizationLanguage
    {
        English,
        French,
        Nellouche,
        Marouche
    }

    public static class LocalizationLanguageExtensions
    {
        public static string ToLanguageCode(this LocalizationLanguage language)
        {
            return language switch
            {
                LocalizationLanguage.English => "en",
                LocalizationLanguage.French => "fr",
                LocalizationLanguage.Nellouche => "nl",
                LocalizationLanguage.Marouche => "mr",
                _ => "en"
            };
        }
    }
}