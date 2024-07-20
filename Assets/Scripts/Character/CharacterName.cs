namespace Character
{
    public enum CharacterName
    {
        Camden,
        Daniel,
        Livia
    };

    // Get string value of enum
    public static class CharacterNameExtensions
    {
        public static string GetString(this CharacterName characterName)
        {
            return characterName switch
            {
                CharacterName.Camden => "Camden",
                CharacterName.Daniel => "Daniel",
                CharacterName.Livia => "Livia",
                _ => "Unknown"
            };
        }
    }
}