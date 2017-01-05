namespace TeamNectarineScheduleManager.Users
{
    // using TeamCalendars;
    using System;

    using Calendars;

    [Serializable()]

    public class Worker : Employee, ILoggable, IEmployee
    {
        private Team team;
        private PersonalCalendar personalCalendar;

        public Worker(string username, string password, string firstName, string lastName)
            : base(username, password, firstName, lastName)
        {
            this.UserType = UserType.Worker;
        }

        public Team Team
        {
            get
            {
                return this.team;
            }

            protected set
            {
                ValidateNull(value, "Worker's team cannot be null!");

                this.Team = value;
            }
        }

        public PersonalCalendar PersonCalendar
        {
            get
            {
                return this.personalCalendar;
            }

            protected set
            {
                ValidateNull(value, "Worker's personal calendar cannot be null!");

                this.personalCalendar = value;
            }
        }

        public void AddTeam(Team team)
        {
            if (this.team != null)
            {
                throw new InvalidOperationException("Adding a team to a worker who already has a team is not allowed");
            }

            this.Team = team;
        }

        private void ValidateNull(object obj, string message = null)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(message);
            }
        }
    }
}
