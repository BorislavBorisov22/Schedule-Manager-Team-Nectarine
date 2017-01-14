namespace TeamNectarineScheduleManager.Calendars
{
    public interface ICalendar
    {
        void AddEvent(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType evt);

        void RemoveEvent(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType evt);

        string[] ToString(int dayOfTheMonth, int month, int year);
    }
}