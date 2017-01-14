namespace TeamNectarineScheduleManager.Users
{
    // using TeamCalendars;
    using System;
    using Calendars;
    using Contracts;
    using Teams;

    [Serializable]

    public class Worker : Employee, ILoggable, IEmployee, IWorker, ISchedulable
    {
        private ICalendar personalCalendar;
        
        public Worker(string username, string password, string firstName, string lastName)
            : base(username, password, firstName, lastName)
        {
            this.UserType = UserType.RegularWorker;
            this.personalCalendar = new PersonalCalendar();
        }

        public ITeam Team { get;  set; }
       
        public void AddEventToCalendar(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType evt)
        {
            this.personalCalendar.AddEvent(dayOfTheMonth, month, year, eventStart, eventEnd, evt);
        }

        public void RemoveEventFromCalendar(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType evt)
        {
            this.personalCalendar.RemoveEvent(dayOfTheMonth, month, year, eventStart, eventEnd, evt);
        }

        public string[] GetEventForDay(int dayOfTheMonth, int month, int year)
        {
            return this.personalCalendar.ToString(dayOfTheMonth, month, year);
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
            if (this.Team != null)
            {
                NFDBTeamName = this.Team.TeamName;
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
            get { return this.Team; }
            set { this.Team = value; }
        }

        #endregion
        // End of the region, needed for DataBase
    }
}
