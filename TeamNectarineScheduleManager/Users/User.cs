namespace TeamNectarineScheduleManager.Users
{
    using System;

    using AppExceptions;
    [Serializable]

    public abstract class User : ILoggable
    {
        // needed for log-in
        private string username;
        private string password;

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public string Username
        {
            get
            {
                return this.username;
            }

            private set
            {
                if (!IsUsernameValid(value))
                {
                    throw new InvalidUsernameException();
                }

                this.username = value;
            }
        }

        // Note: if you need access to the password from outside the user class:
        // change the return value of the getter to "this.password"
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (!IsPasswordValid(value))
                {
                    throw new InvalidPasswordException();
                }

                this.password = value;
            }
        }

        public UserType UserType { get; protected set; }

        private bool IsUsernameValid(string name)
        {
            if (name.Length < 5)
            {
                return false;
            }

            foreach (var symbol in name)
            {
                if (!char.IsDigit(symbol) && !char.IsLetter(symbol) && symbol != '.' && symbol != '_')
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsPasswordValid(string password)
        {
            int lettersCount = 0;
            int digitsCount = 0;

            foreach (var symbol in password)
            {
                if (char.IsDigit(symbol))
                {
                    digitsCount++;
                }
                else if (char.IsLetter(symbol))
                {
                    lettersCount++;
                }
            }

            if (lettersCount < 1 || digitsCount < 5)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return string.Format("Username: {0}, Pass: {1}", this.Username, new string('*', this.password.Length));
        }

        // Needed for DataBase
        #region NeededForDataBase
        public string NFDBPasswordCheckBypass
        {
            get { return password; }
            set { password = value; }
        }
        #endregion
        // End of region, needed for DataBase

    }
}
