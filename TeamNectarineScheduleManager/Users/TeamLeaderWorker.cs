namespace TeamNectarineScheduleManager.Users
{
    public class TeamLeaderWorker : Worker, ILoggable, IEmployee
    {
        public TeamLeaderWorker(string username, string password, string firstName, string lastName) 
            : base(username, password, firstName, lastName)
        {
        }
    }
}
