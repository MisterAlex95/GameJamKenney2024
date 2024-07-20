namespace Journal
{
    public enum JournalActivityName
    {
        None,
        Sleep,
        Cinema,
        Eat,
        Read,
        Clean,
        Cook,
    }

    public static class JournalActivityNameExtensions
    {
        public static string ToFriendlyString(this JournalActivityName activity)
        {
            return activity switch
            {
                JournalActivityName.None => "None",
                JournalActivityName.Sleep => "Sleep",
                JournalActivityName.Cinema => "Cinema",
                JournalActivityName.Eat => "Eat",
                JournalActivityName.Read => "Read",
                JournalActivityName.Clean => "Clean",
                JournalActivityName.Cook => "Cook",
                _ => "Unknown"
            };
        }
    }
}