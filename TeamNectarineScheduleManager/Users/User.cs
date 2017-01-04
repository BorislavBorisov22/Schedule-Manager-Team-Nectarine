namespace TeamNectarineScheduleManager.Users
{
    using System;

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
                    throw new ArgumentException("Invalid username! Username should be at least 5 symbols long and can contain only letters digits or dot symbols");
                }

                this.username = value;
            }
        }

        // Note: if you need access to the passwod from outside the user class:
        // change the return value of the getter to "this.password"
        public string Password
        {
            get
            {
                return this.password;
            }

            private set
            {
                if (!IsPasswordValid(value))
                {
                    throw new ArgumentException("Invalid password! Password should contain at least one letter symbol and at least 5 digit symbols");
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
    }
}
