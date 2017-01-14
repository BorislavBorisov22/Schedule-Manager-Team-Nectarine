namespace TeamNectarineScheduleManager.Users
{
    using System;
    [Serializable]
    public class GuestUser : User, ILoggable
    {
        public GuestUser(string username, string password) 
            : base(username, password)
        {
        }

        public string GetCompanyInfo()
        {
            return "";
        }
    }
}
