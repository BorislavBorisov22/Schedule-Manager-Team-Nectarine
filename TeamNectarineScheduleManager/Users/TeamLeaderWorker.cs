namespace TeamNectarineScheduleManager.Users
{
    using System;
    using Calendars;
    using Teams;

    [Serializable]

    public class TeamLeaderWorker : Worker, ILoggable, IEmployee
    {
        public TeamLeaderWorker(string username, string password, string firstName, string lastName, string teamName = "Global Team")
            : base(username, password, firstName, lastName)
        {
            if (this.Team == null)
            {
                this.Team = new Team(teamName, this);
            }
        }

        public void ReleaseMemberFromTeam(RegularWorker memberToRelease)
        {
            this.Team.RemoveMemeber(memberToRelease);
        }

        public void AddMemberToTeam(RegularWorker memberToAdd)
        {
            this.Team.AddMember(memberToAdd);
        }

        public void AddEventToTeamCalendar(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType evt)
        {
            this.Team.AddEventToCalendar(dayOfTheMonth, month, year, eventStart, eventEnd, evt);
        }

        public void RemoveEventFromTeamCalendar(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType evt)
        {
            this.Team.RemoveEventFromCalendar(dayOfTheMonth, month, year, eventStart, eventEnd, evt);
        }
    }
}