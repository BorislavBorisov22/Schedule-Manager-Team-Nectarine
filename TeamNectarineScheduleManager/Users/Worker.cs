namespace TeamNectarineScheduleManager.Users
{
    // using TeamCalendars;
    using System.Security;

    public class Worker : Employee, Iloggable, IEmployee
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
