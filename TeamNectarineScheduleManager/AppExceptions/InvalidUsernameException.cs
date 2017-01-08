namespace TeamNectarineScheduleManager.AppExceptions
{
    using System;

    public class InvalidUsernameException : ApplicationException
    {
        public const string defaultMessage =
            "Invalid username! Username should be at least 5 symbols long and can contain only letters digits or dots or underscores.";

        public InvalidUsernameException(string message)
            : base(message)
        {

        }

        public InvalidUsernameException()
            : base(defaultMessage)
        {

        }
    }
}
