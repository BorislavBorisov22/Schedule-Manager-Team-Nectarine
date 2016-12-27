namespace TeamNectarineScheduleManager.Users
{
    public class Worker : Employee, Iloggable
    {
        private Team team;
        private ContractType contract;

        public Worker(string username, string password, string firstName, string lastName)
            : base(username, password, firstName, lastName)
        {
        }
    }
}
