namespace TeamNectarineScheduleManager.Users
{
    public class TeamLeaderWorker : Worker, Iloggable
    {
        public TeamLeaderWorker(string username, string password, string firstName, string lastName) 
            : base(username, password, firstName, lastName)
        {
        }
    }
}
