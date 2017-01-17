namespace TeamNectarineScheduleManager.Users
{
    using TeamNectarineScheduleManager.Teams;

    public interface IRegularWorker : IWorker
    {
        string TeamInfo { get; }

        void JoinTeam(ITeam team);

        void QuitTeam();
    }
}