namespace TeamNectarineScheduleManager.Users
{
    // using TeamCalendars;

    [Serializable()]
    public class Worker : Employee, ILoggable, IEmployee
    {
        private Team team;
        private ContractType contract;
        // private PersonalCalendar personalCalendar;

        public Worker(string username, string password, string firstName, string lastName)
            : base(username, password, firstName, lastName)
        {
        }
    }
}
