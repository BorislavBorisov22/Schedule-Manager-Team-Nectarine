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
        
        public void AddEventToUser(Worker worker, int dayOfMonth, int month, int year, string startTIme, string endTime, EventType evt)
        {
            worker.AddEventToCalendar(dayOfMonth,month,year,startTIme,endTime,evt);
        }

        public void RemoveEventFromUser(Worker worker, int dayOfMonth, int month, int year, string startTIme, string endTime, EventType evt)
        {
            worker.RemoveEventFromCalendar(dayOfMonth, month, year, startTIme, endTime, evt);
        }
    }
}
