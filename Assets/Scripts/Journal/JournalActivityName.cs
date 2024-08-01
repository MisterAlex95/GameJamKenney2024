using System;
using Localization;

namespace Journal
{
    public enum JournalActivityName
    {
        None,
        Breakfast,
        Sleeping,
        Cleaning,
        Reading,
        Cooking,
        Lunch,
        Restaurant,
        Cinema
    }

    public static class JournalActivityNameExtensions
    {
        public static int ToFriendlyString(this JournalActivityName activity)
        {
            return activity switch
            {
                JournalActivityName.None => 27,
                JournalActivityName.Breakfast => 28,
                JournalActivityName.Sleeping => 29,
                JournalActivityName.Cleaning => 30,
                JournalActivityName.Reading => 31,
                JournalActivityName.Cooking => 33,
                JournalActivityName.Lunch => 34,
                JournalActivityName.Restaurant => 32,
                JournalActivityName.Cinema => 35,
                _ => 0
            };
        }

        public static TOutput FromFriendlyString<TOutput>(string optionText)
        {
            return (TOutput)Enum.Parse(typeof(JournalActivityName), optionText);
        }
    }
}