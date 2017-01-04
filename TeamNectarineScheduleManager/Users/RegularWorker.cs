namespace TeamNectarineScheduleManager.Users
{
    using System;
    [Serializable()]
    public class RegularWorker : Worker, ILoggable, IEmployee
    {
        public RegularWorker(string username, string password, string firstName, string lastName)
            : base(username, password, firstName, lastName)
        {
        }
    }
}
