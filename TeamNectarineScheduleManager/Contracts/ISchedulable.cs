using TeamNectarineScheduleManager.Calendars;

namespace TeamNectarineScheduleManager.Contracts
{
    public interface ISchedulable
    {
        void AddEventToCalendar(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType evt);

        void RemoveEventFromCalendar(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType eventType);

        string[] GetEventForDay(int dayOfTheMonth, int month, int year);     
    }
}
