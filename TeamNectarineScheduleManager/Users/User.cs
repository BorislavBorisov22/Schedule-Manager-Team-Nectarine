namespace TeamNectarineScheduleManager.Users
{
    using System;
    using System.Security;

    public abstract class User : Iloggable
    {
        private string username; // needed for log-in
        private string password; // needed for log-in

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
                return new string('*', this.password.Length);
            }

            private set
            {
                if (!IsPasswodValid(value))
                {
                    throw new ArgumentException("Invalid password! Password should contain at least one letter symbol and at least 5 digit symbols");
                }

                this.password = value;
            }
        }

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

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

        private bool IsPasswodValid(string password)
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

            if (lettersCount < 1 && digitsCount < 5)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return string.Format("Username: {0}, Pass: {1}", this.Username, this.Password);
        }
    }
}
