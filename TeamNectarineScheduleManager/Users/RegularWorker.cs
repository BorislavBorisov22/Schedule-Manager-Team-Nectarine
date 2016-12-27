namespace TeamNectarineScheduleManager.Users
{
    public class RegularWorker : Worker, Iloggable
    {
        public RegularWorker(string username, string password, string firstName, string lastName) 
            : base(username, password, firstName, lastName)
        {
        }

    }
}
