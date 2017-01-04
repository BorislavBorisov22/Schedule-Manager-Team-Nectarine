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
        private static bool idCorrect;
        private static bool pwdCorrect;
        private static bool adminLogin;
        private static bool userLogin;
        private static string username;
        private static string password;

        public static void ShowMainMenu()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.WindowHeight);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║ Welcome to Team Nectarine's Schedule Manager! ║");
            Console.WriteLine("║    [1] to log in as administrator.            ║");
            Console.WriteLine("║    [2] to log in as user.                     ║");
            Console.WriteLine("║    [Esc] to quit.                             ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");
            Console.WriteLine("Make your selection: ");
            ConsoleKeyInfo cki;

            do
            {
                cki = Console.ReadKey(true);

                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                        adminLogin = true;
                        AdminLogin();
                        break;
                    case ConsoleKey.D2:
                        userLogin = true;
                        UserLogin();
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        Console.WriteLine("Wrong selection. Try again: ");
                        break;
                }
            } while (cki.Key != ConsoleKey.D1 && cki.Key != ConsoleKey.D2);

            while (!idCorrect || !pwdCorrect)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nWrong credentials. Try again.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                UserLogin();
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
            Console.WriteLine("What do you want to do next: ");
            Console.WriteLine("     [1] Check daily schedule");
            Console.WriteLine("     [2] Check weekly schedule");
            Console.WriteLine("     [3] Log-out user");
            Console.WriteLine("     [Esc] to quit");
            ConsoleKeyInfo cki;

            var worker = new Worker("cenko91", "blabla12345", "Cenko", "Chokov");
            do
            {
                cki = Console.ReadKey(true);

                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                        DisplayScheduleDay(worker);
                        ShowUserMenu();
                        break;
                    case ConsoleKey.D2:
                        DisplayScheduleWeek(worker);
                        ShowUserMenu();
                        break;
                    case ConsoleKey.D3:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Log-out success!");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        ShowMainMenu();
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        Console.WriteLine("Wrong selection. Try again: ");
                        break;
                }
            } while (cki.Key != ConsoleKey.D1 && cki.Key != ConsoleKey.D2 && cki.Key != ConsoleKey.D3);
        }

        private static void ShowAdminMenu()
        {
            throw new NotImplementedException();
        }

        private static void UserLogin()
        {
            Console.Write("Username: ");
            GetUsername();
            Console.Write("Password: ");
            GetPassword();
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
                    username = id;
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
                    password = pwd;
                }
            }
        }

        private static void DisplayScheduleWeek(Worker worker)
        {
            Dictionary<string, string> activities = new Dictionary<string, string>();
            activities.Add("In Training", "10:00 - 14:00");
            activities.Add("Backoffice", "14:00 - 15:00");
            activities.Add("Lunch", "15:00 - 16:00");
            activities.Add("Break", "17:00 - 17:10");
            activities.Add("Party", "19:00 - 22:00");

            var tableWeek = new TableWeek(activities);
            tableWeek.FillAndShow();
        }

        private static void DisplayScheduleDay(Worker worker)
        {
            //foreach (var someEvent in worker.PersonalCalendar.GetDailySchedule(52, DayOfWeek.Monday))
            //{
            //    Console.WriteLine(someEvent.EventType);
            //    Console.WriteLine($"{someEvent.EventStart} - {someEvent.EventEnd}");
            //}

            Dictionary<string, string> activities = new Dictionary<string, string>();
            activities.Add("In Training", "10:00 - 14:00");
            activities.Add("Backoffice", "14:00 - 15:00");
            activities.Add("Lunch", "15:00 - 16:00");
            activities.Add("Break", "17:00 - 17:10");
            activities.Add("Party", "19:00 - 22:00");

            var tableDay = new TableDay(activities);
            tableDay.FillAndShow();
        }
    }
}