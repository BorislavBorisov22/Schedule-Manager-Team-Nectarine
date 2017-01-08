namespace TeamNectarineScheduleManager.AppExceptions
{
    using System;

    public class InvalidPasswordException : ApplicationException
    {
        public const string defaultMessage =
            "Invalid password! Password should contain at least 5 digits and one letter";

        public InvalidPasswordException(string message)
            : base(message)
        {
        }

        public InvalidPasswordException()
            : base(defaultMessage)
        {

        }
    }
}
