using TeamNectarineScheduleManager.Calendars;

namespace TeamNectarineScheduleManager.Users
{
    public interface IWorker
    {
        ITeam NFDBTeamCheckBypass { get; set; }
        PersonalCalendar PersonCalendar { get; set; }
        string Team { get; }

        void AddEventToCalendar(DailyEvent workerDailyEvent);
        void AddToTeam(ITeam team);
        string NFDBGetTeamName();
        void NFDBSetTeamName();
        void RemoveFromTeam();
    }
}