namespace TeamNectarineScheduleManager
{
    using System.Collections.Generic;
    using TeamNectarineScheduleManager.Users;

    public interface ITeam
    {
        List<Worker> Members { get; }

        int MembersCount { get; }

        TeamLeaderWorker TeamLeader { get; set; }

        string TeamName { get; }

        void AddMember(RegularWorker worker);
        void NFDBClearMembersAndLeader();

        List<string> NFDBGetRegularWorkerNames();

        string NFDBGetTeamLeaderName();

        void NFDBSetRegularWorkerNames();

        void NFDBSetTeamLeaderName();

        void RemoveMemeber(RegularWorker worker);
    }
}