namespace TeamNectarineScheduleManager.Users
{
    public interface IEmployee : ILoggable
    {
        string FirstName { get; }

        string LastName { get; }
    }
}