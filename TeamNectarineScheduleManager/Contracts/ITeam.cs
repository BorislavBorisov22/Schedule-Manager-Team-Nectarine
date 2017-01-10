namespace TeamNectarineScheduleManager
{
    using System.Collections.Generic;
    using TeamNectarineScheduleManager.Users;

    public interface ITeam
    {
        ICollection<IWorker> Members { get; }

        int MembersCount { get; }

        TeamLeaderWorker TeamLeader { get; set; }

        string TeamName { get; }

        void AddMember(IWorker worker);

        void NFDBClearMembersAndLeader();

        List<string> NFDBGetRegularWorkerNames();

        void RemoveMemeber(RegularWorker worker);

        string NFDBGetTeamLeaderName();

        void NFDBSetRegularWorkerNames();

        void NFDBSetTeamLeaderName();

    }
}