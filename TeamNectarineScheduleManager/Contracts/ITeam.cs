namespace TeamNectarineScheduleManager.Teams
{
    using System.Collections.Generic;
    using TeamNectarineScheduleManager.Calendars;
    using TeamNectarineScheduleManager.Users;

    public interface ITeam
    {
        ICollection<IWorker> Members { get; }

        int MembersCount { get; }

        TeamLeaderWorker TeamLeader { get; set; }
        string TeamName { get; }

        void AddEventToCalendar(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType evt);

        void AddMember(RegularWorker worker);

        string[] GetEventForDay(int dayOfTheMonth, int month, int year);

        void NFDBClearMembersAndLeader();

        List<string> NFDBGetRegularWorkerNames();

        string NFDBGetTeamLeaderName();

        void NFDBSetRegularWorkerNames();

        void NFDBSetTeamLeaderName();

        void RemoveEventFromCalendar(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType evt);

        void RemoveMemeber(RegularWorker worker);
    }
}