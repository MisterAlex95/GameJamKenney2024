using System;

namespace Journal
{
    public enum JournalActivityName
    {
        None,
        Breakfast,
        Sleeping,
        Cleaning,
        Movies,
        Reading,
        Cooking,
        Lunch,
        Restaurant,
        Cinema
    }

    public static class JournalActivityNameExtensions
    {
        public static string ToFriendlyString(this JournalActivityName activity)
        {
            return activity switch
            {
                JournalActivityName.None => "None",
                JournalActivityName.Breakfast => "Breakfast",
                JournalActivityName.Sleeping => "Sleeping",
                JournalActivityName.Cleaning => "Cleaning",
                JournalActivityName.Movies => "Movies",
                JournalActivityName.Reading => "Reading",
                JournalActivityName.Cooking => "Cooking",
                JournalActivityName.Lunch => "Lunch",
                JournalActivityName.Restaurant => "Restaurant",
                JournalActivityName.Cinema => "Cinema",
                _ => "Unknown"
            };
        }

        public static TOutput FromFriendlyString<TOutput>(string optionText)
        {
            return (TOutput) Enum.Parse(typeof(JournalActivityName), optionText);
        }
    }
}