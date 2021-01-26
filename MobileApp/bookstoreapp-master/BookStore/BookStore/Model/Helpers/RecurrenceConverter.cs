using System;
using static BookStore.Model.Enums;

namespace BookStore.Model.Helpers
{
    public static class RecurrenceConverter
    {
        public static Recurrence StringToRecurrence(string recurrence)
        {
            switch (recurrence)
            {
                case "weekly":
                    return Recurrence.Weekly;
                case "monthly":
                    return Recurrence.Monthly;
                case "quarterly":
                    return Recurrence.Quarterly;
                default:
                    return Recurrence.Yearly;
            }
        }
    }
}
