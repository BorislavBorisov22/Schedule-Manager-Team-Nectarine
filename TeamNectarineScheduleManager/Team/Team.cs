namespace TeamNectarineScheduleManager
{
    using System.Collections.Generic;
    using System.Text;
    using Users;

    // the team class holds a sequence of members and a team leader and has two basic 
    // functionalities -> adding a member to the team and removing a member from the team
    // TO DO: add a calnedar for the team that includes the duties of all of the 
    // team members in the team
    public class Team
    {
        private IList<Worker> members;

        public Team(Worker teamLeader)
        {
            this.TeamName = "Global team";
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
                return this.members.Count + 1;
            }
        }

        public List<Worker> Members
        {
            get
            {
                return new List<Worker>(this.members);
            }
        }

        public TeamLeaderWorker TeamLeader { get; private set; }

        public string TeamName { get; private set; }

        public void AddMember(RegularWorker worker)
        {
            this.members.Add(worker);
        }

        public void RemoveMemeber(RegularWorker worker)
        {
            this.members.Remove(worker);
        }
    }
}
