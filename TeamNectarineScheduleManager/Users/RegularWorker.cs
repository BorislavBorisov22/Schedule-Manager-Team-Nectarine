namespace TeamNectarineScheduleManager.Users
{
    using Calendars;
    using System;
    using Contracts;
    using Teams;

    [Serializable]

    public class RegularWorker : Worker, ILoggable, IEmployee, IRegularWorker
    {
        public RegularWorker(string username, string password, string firstName, string lastName ,Team team = null)
            : base(username, password, firstName, lastName)
        {
            this.Team = team;
        }      

        public string TeamInfo
        {
            get
            {
                if (this.Team == null)
                {
                    return "No Team";
                }

                return this.Team.TeamName;
            }
        }

        public void JoinTeam(ITeam team)
        {
            this.Team = team;
        }

        public void QuitTeam()
        {
            this.Team = null;
        }
    }
}
