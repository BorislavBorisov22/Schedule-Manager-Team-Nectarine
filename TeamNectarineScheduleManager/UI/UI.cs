namespace TeamNectarineScheduleManager.UserInterface
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using DataBaseLibrary;
    using Table;
    using Users;
    using Teams;
    using System.Text;

    public static class UI
    {
        private static bool adminLogin, userLogin;

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
            // create sample team and workers, and save them to test the login
            var teamLeader = new TeamLeaderWorker("IvanIvanov13", "shef4eto1234", "Ivan", "Ivanov");
            var teamMemeber = new RegularWorker("PepoPepov22", "pepun55555", "Petar", "Petrov");
            var anotherTeamMember = new RegularWorker("Stamito333", "staM17000.", "Stamatka", "Chereshova");
            var team = new Team("Nectarine", teamLeader);
            team.AddMember(teamMemeber);
            team.AddMember(anotherTeamMember);
            DataBase.Save(team);
            DataBase.Save(teamLeader);
            DataBase.Save(teamMemeber);
            DataBase.Save(anotherTeamMember);

            Console.Write("\nUsername: ");
            var id = Console.ReadLine();
            Console.Write("Password: ");
            var pwd = GetConsolePassword();

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

        private static string GetConsolePassword()
        {
            var sb = new StringBuilder();
            while (true)
            {
                var cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }

                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.Write("\b\0\b");
                        sb.Length--;
                    }

                    continue;
                }

                Console.Write('*');
                sb.Append(cki.KeyChar);
            }

            return sb.ToString();
        }
    }
}