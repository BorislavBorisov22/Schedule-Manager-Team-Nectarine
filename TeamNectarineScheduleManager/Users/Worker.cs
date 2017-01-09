namespace TeamNectarineScheduleManager.Users
{
    // using TeamCalendars;
    using System;
    using Calendars;

    [Serializable]

    public class Worker : Employee, ILoggable, IEmployee, IWorker
    {
        private ITeam team;
        private PersonalCalendar personalCalendar;

        public Worker(string username, string password, string firstName, string lastName)
            : base(username, password, firstName, lastName)
        {
            this.UserType = UserType.RegularWorker;
            this.PersonCalendar = new PersonalCalendar();
        }

        public string Team
        {
            get
            {
                if (this.team == null)
                {
                    return "No current Team";
                }

                return this.team.TeamName;
            }
        }
       
        public PersonalCalendar PersonCalendar
        {
            get
            {
                return this.personalCalendar;
            }

            set
            {
                ValidateNull(value, "Worker's personal calendar cannot be null!");

                this.personalCalendar = value;
            }
        }

        public void AddToTeam(ITeam team)
        {
            if (this.team != null)
            {
                throw new InvalidOperationException("Adding a team to a worker who already has a team is not allowed");
            }

            if (team == null)
            {
                throw new ArgumentNullException("Cannot add null value to worker's team");
            }

            this.team = team;
        }

        public void RemoveFromTeam()
        {
            this.team = null;
        }

        public void AddEventToCalendar(DailyEvents workerDailyEvent)
        {
            this.PersonCalendar.AddEvent(workerDailyEvent);
        }

        private void ValidateNull(object obj, string message = null)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(message);
            }
        }

        // Needed for DataBase
        #region NeededForDataBase
        private string NFDBTeamName = "";

        public void NFDBSetTeamName()
        {
            if (team != null)
            {
                NFDBTeamName = team.TeamName;
            }
            else
            {
                NFDBTeamName = "";
            }
        }

        public string NFDBGetTeamName()
        {
            return NFDBTeamName;
        }

        public ITeam NFDBTeamCheckBypass
        {
            get { return team; }
            set { team = value; }
        }

        #endregion
        // End of the region, needed for DataBase
    }
}
