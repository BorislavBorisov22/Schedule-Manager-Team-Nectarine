namespace TeamNectarineScheduleManager.Teams
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Users;

    // the team class holds a sequence of members and a team leader and has two basic 
    // functionalities -> adding a member to the team and removing a member from the team
    // TO DO: add a calendar for the team that includes the duties of all of the 
    // team members in the team
    [Serializable]

    public class Team : ITeam
    {
        private const string defaultTeamName = "Global team"; 

        private ICollection<IWorker> members;

        public Team(TeamLeaderWorker teamLeader)
            :this(defaultTeamName, teamLeader)
        {   
        }

        public Team(string teamName, TeamLeaderWorker teamLeader)
        {
            this.members = new List<Worker>();
            this.TeamName = teamName;
            this.TeamLeader = teamLeader;
            this.members.Add(this.TeamLeader);
        }

        public int MembersCount
        {
            get
            {
                return this.members.Count;
            }
        }

        public ICollection<IWorker> Members
        {
            get
            {
                return new List<Worker>(this.members);
            }
        }

        public TeamLeaderWorker TeamLeader { get; set; }

        public string TeamName { get; private set; }

        public void AddMember(IWorker worker)
        {
            this.members.Add(worker);
            worker.AddToTeam(this);
        }

        public void RemoveMemeber(RegularWorker worker)
        {
            this.members.Remove(worker);
            worker.RemoveFromTeam();
        }

        // Needed for DataBase
        #region NeededForDataBase.

        private List<string> NFDBRegularWorkerNames = new List<string>();
        private string NFDBTeamLeaderName;

        public List<string> NFDBGetRegularWorkerNames()
        {
            return NFDBRegularWorkerNames;
        }

        public void NFDBSetRegularWorkerNames()
        {
            NFDBRegularWorkerNames.Clear();

            foreach (var member in this.members)
            {
                NFDBRegularWorkerNames.Add(member.Username);
            }
        }

        public string NFDBGetTeamLeaderName()
        {
            return NFDBTeamLeaderName;
        }

        public void NFDBSetTeamLeaderName()
        {
            NFDBTeamLeaderName = TeamLeader.Username;
        }

        public void NFDBClearMembersAndLeader()
        {
            members.Clear();
            TeamLeader = null;
        }

        #endregion 
        // Needed for DataBase END
    }
}
