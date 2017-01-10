namespace TeamNectarineScheduleManager.UserInterface
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using DataBaseLibrary;
    using Table;
    using Users;

    public static class UI
    {
        private static bool idCorrect, pwdCorrect, adminLogin, userLogin;

        public static void ShowMainMenu()
        {
            Console.SetWindowSize(150, Console.WindowHeight);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║ Welcome to Team Nectarine's Schedule Manager! ║");
            Console.WriteLine("║    [1] to log in as administrator.            ║");
            Console.WriteLine("║    [2] to log in as user.                     ║");
            Console.WriteLine("║    [Esc] to quit.                             ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");
            Console.WriteLine("Make your selection:");
            ConsoleKeyInfo cki;
            bool showMainMenu = true;

            while (showMainMenu)
            {
                cki = Console.ReadKey();

                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                        adminLogin = true;
                        showMainMenu = false;
                        AdminLogin();
                        break;
                    case ConsoleKey.D2:
                        userLogin = true;
                        showMainMenu = false;
                        UserLogin();
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        Console.WriteLine("Wrong selection. Try again: ");
                        break;
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nLogin success!");
            Console.ForegroundColor = ConsoleColor.Cyan;

            if (userLogin)
            {
                ShowUserMenu();
            }
            else if (adminLogin)
            {
                ShowAdminMenu();
            }
        }

        private static void ShowUserMenu()
        {
            bool showUserMenu = true;
            ConsoleKeyInfo cki;
            while (showUserMenu)
            {
                Console.WriteLine("[1] Check daily schedule");
                Console.WriteLine("[2] Check weekly schedule");
                Console.WriteLine("[3] Log-out user");
                Console.WriteLine("[Esc] to quit");
                Console.WriteLine("Choose what to do next:");

                cki = Console.ReadKey();

                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                        DisplayScheduleDay();
                        break;
                    case ConsoleKey.D2:
                        DisplayScheduleWeek();
                        break;
                    case ConsoleKey.D3:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nLog-out success!");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        ShowMainMenu();
                        showUserMenu = false;
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        Console.WriteLine("Wrong selection. Try again: ");
                        break;
                }
            }
        }

        private static void ShowAdminMenu()
        {
            throw new NotImplementedException();
        }

        private static void UserLogin()
        {
            // create sample worker to test user login
            RegularWorker rw = new RegularWorker("stamo99", "stamat12345", "Stamat", "Haralambov");
            DataBase.Save(rw);

            Console.Write("\nUsername: ");
            //GetUsername();
            var id = Console.ReadLine();
            Console.Write("Password: ");
            //GetPassword();
            var pwd = Console.ReadLine();

            while (!DataBase.IsValidLoginData(id, pwd))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nWrong credentials. Try again:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                UserLogin();
            }
        }

        private static void AdminLogin()
        {
            throw new NotImplementedException();
        }

        private static void GetUsername()
        {
            var id = Console.ReadLine();
            CheckUsername(id);
        }

        private static void GetPassword()
        {
            string pwd = null;

            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);

                if (cki.Key == ConsoleKey.Enter)
                {
                    if (pwd.Length < 4)
                    {
                        Console.Write("\nPassword must be at least 4 characters long: ");
                        GetPassword();
                    }

                    CheckPassword(pwd);
                    break;
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    if (pwd.Length > 0)
                    {
                        pwd.Remove(pwd.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    pwd = pwd + cki.KeyChar;
                    Console.Write("*");
                }
            }
        }

        private static void CheckUsername(string id)
        {
            using (var sr = new StreamReader("login-user.txt"))
            {
                if (sr.ReadToEnd().Contains(id))
                {
                    idCorrect = true;
                }
            }
        }

        private static void CheckPassword(string pwd)
        {
            using (var sr = new StreamReader("login-user.txt"))
            {
                if (sr.ReadToEnd().Contains(pwd.ToString()))
                {
                    pwdCorrect = true;
                }
            }
        }

        private static void DisplayScheduleWeek()
        {
            // sample activities
            var activities = new Dictionary<string, string>();
            activities.Add("In Training", "10:00 - 14:00");
            activities.Add("Backoffice", "14:00 - 15:00");
            activities.Add("Lunch", "15:00 - 16:00");
            activities.Add("Break", "17:00 - 17:10");
            activities.Add("Party", "19:00 - 22:00");

            Table tableWeek = new TableWeek(activities);
            tableWeek.FillAndDraw();
        }

        private static void DisplayScheduleDay()
        {
            //foreach (var someEvent in worker.PersonalCalendar.GetDailySchedule(52, DayOfWeek.Monday))
            //{
            //    Console.WriteLine(someEvent.EventType);
            //    Console.WriteLine($"{someEvent.EventStart} - {someEvent.EventEnd}");
            //}

            var activities = new Dictionary<string, string>();
            activities.Add("In Training", "10:00 - 14:00");
            activities.Add("Backoffice", "14:00 - 15:00");
            activities.Add("Lunch", "15:00 - 16:00");
            activities.Add("Break", "17:00 - 17:10");
            activities.Add("Party", "19:00 - 22:00");

            Table tableDay = new TableDay(activities);
            tableDay.FillAndDraw();
        }
    }
}