namespace TeamNectarineScheduleManager.Users
{
    public interface ILoggable
    {
        string Username { get; }

        string Password { get; }

        UserType UserType { get; }
    }
}
