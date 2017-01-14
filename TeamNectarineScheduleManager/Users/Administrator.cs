namespace TeamNectarineScheduleManager.Users
{
    using Calendars;
    using System;

    [Serializable]
    public class Administrator : Employee, ILoggable, IEmployee
    {
        private CompanyCalendar companyCalendar;

        public Administrator(string username, string password, string firstName, string lastName) 
            : base(username, password, firstName, lastName)
        {
            this.UserType = UserType.Admin;
        }
        
        // methods
        //public void ShowCompanyCalendar(Day day)
        //{
        //    throw new NotImplementedException();
        //}

        //public void ShowCompanyCalendar(Week week)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
