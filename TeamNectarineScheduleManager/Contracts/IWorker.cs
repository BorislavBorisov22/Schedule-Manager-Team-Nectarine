namespace TeamNectarineScheduleManager.Users
{
    using TeamNectarineScheduleManager.Calendars;
    using TeamNectarineScheduleManager.Contracts;
    using Teams;

    public interface IWorker : IEmployee
    {
        ITeam NFDBTeamBypass { get; set; }

        void AddEventToCalendar(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType evt);

        string[] GetEventForDay(int dayOfTheMonth, int month, int year);

        void RemoveEventFromCalendar(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType evt);

        string NFDBGetTeamName();

        void NFDBSetTeamName();

    }
}