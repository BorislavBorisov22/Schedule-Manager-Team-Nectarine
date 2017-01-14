namespace TeamNectarineScheduleManager.Users
{
    using TeamNectarineScheduleManager.Teams;

    public interface IRegularWorker
    {
        string TeamInfo { get; }

        void JoinTeam(ITeam team);

        void QuitTeam();
    }
}