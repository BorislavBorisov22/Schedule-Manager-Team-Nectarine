namespace TeamNectarineScheduleManager.UserInterface
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using DataBaseLibrary;
    using Table;
    using Teams;
<<<<<<< HEAD
    using System.Text;
    using Calendars;
=======
    using Users;
>>>>>>> 45c24c04422a0ae50985e63da10e5059f4805c21

    public static class UI
    {
        private static bool adminLogin, userLogin;
        private static RegularWorker LoggedUser = null;
        private static Administrator LoggedAdmin = null;

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
                Console.WriteLine("[3] Add task to daily schedule");
                Console.WriteLine("[4] Log-out user");
                Console.WriteLine("[Esc] to quit");
                Console.WriteLine("Choose what to do next:");

                cki = Console.ReadKey();

                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                        DisplayScheduleDay(LoggedUser);
                        break;
                    case ConsoleKey.D2:
                        DisplayScheduleWeek();
                        break;
                    case ConsoleKey.D3:
                        AddToWorkersSchedule(LoggedUser);
                        break;
                    case ConsoleKey.D4:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nLog-out success!");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        showUserMenu = false;
                        ShowMainMenu();
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
            bool showAdminMenu = true;
            ConsoleKeyInfo cki;
            while (showAdminMenu)
            {
                Console.WriteLine("[1] Check user's daily schedule");
                Console.WriteLine("[2] Add task to user's schedule");
                Console.WriteLine("[3] Show all workers in the company");
                Console.WriteLine("[4] Log-out user");
                Console.WriteLine("[Esc] to quit");
                Console.WriteLine("Choose what to do next:");

                cki = Console.ReadKey();

                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                        {
                            Console.WriteLine();
                            Console.Write("Enter a workers username to see his daily schedule:");
                            string targetUserName = Console.ReadLine();

                            if (UserExists(targetUserName))
                            {
                                var loadedUser = DataBase.LoadRegularWorker(targetUserName);
                                DisplayScheduleDay(loadedUser);
                            }
                            break;
                        }
                       
                    case ConsoleKey.D2:
                        {
                            Console.WriteLine();
                            Console.WriteLine("Enter username to add task to:");
                            string targetUserName = Console.ReadLine();
                            if (UserExists(targetUserName))
                            {
                                var loadedUser = DataBase.LoadRegularWorker(targetUserName);
                                AddToWorkersSchedule(loadedUser);
                            }
                            break;
                        }
                    case ConsoleKey.D3:
                        ListAllWorkers();
                        break;
                    case ConsoleKey.D4:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nLog-out success!");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        ShowMainMenu();
                        showAdminMenu = false;
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        Console.WriteLine("Wrong selection. Try again: ");
                        break;
                }
            }
        }

        private static void GetWorkersDailySchedule()
        {
            Console.WriteLine();
            Console.WriteLine("Enter worker's username to see his schedule:");

            string targetUsername = Console.ReadLine();

            if (!DataBase.UserExists(targetUsername))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no such user in the company!");
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else
            {
                var loadedTargetUser = DataBase.LoadRegularWorker(targetUsername);
                Console.WriteLine("{0}'s daily schedule", targetUsername);
                DisplayScheduleDay(loadedTargetUser);
            }
        }

        private static void AddToWorkersSchedule(RegularWorker toAddTaskTo)
        { 

            Console.WriteLine("Enter a date to add task to:");
            DateTime addedDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Choose a task starting time, ending time and a type from the list below:");
            Console.WriteLine("-> TeamworkDefence");
            Console.WriteLine("-> Work");
            Console.WriteLine("-> Lunch");
            Console.WriteLine("-> Break");
            Console.WriteLine("-> InTraining");

            var taskToAdd = Console.ReadLine().Split(' ');
            string startTime = taskToAdd[0];
            string endTime = taskToAdd[1];
            string taskType = taskToAdd[2];

            toAddTaskTo.AddEventToCalendar(
                addedDate.Day,
                addedDate.Month,
                addedDate.Year,
                startTime,
                endTime,
                (EventType)Enum.Parse(typeof(EventType), taskType));

            DataBase.Save(toAddTaskTo);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task successfully added to {0}", toAddTaskTo.Username);
            Console.ForegroundColor = ConsoleColor.Cyan;

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

            LoggedUser = DataBase.LoadRegularWorker(id);


        }

        private static void AdminLogin()
        {
            Console.Write("\nUsername: ");
            var id = Console.ReadLine();
            Console.Write("Password: ");
            var pwd = GetConsolePassword();

            while (!DataBase.IsValidLoginData(id, pwd))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nWrong credentials. Try again:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                AdminLogin();
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

        private static void DisplayScheduleDay(Worker worker)
        {
            var currentDate = DateTime.Today;
            var dailyEventsForWorker = worker.GetEventForDay(currentDate.Day, currentDate.Month, currentDate.Year);

            if (dailyEventsForWorker.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} has no tasks for the day", worker.Username);
                Console.ForegroundColor = ConsoleColor.Cyan;

            }
            else
            {
                var activitiesToShow = new Dictionary<string, string>();

                foreach (var evt in dailyEventsForWorker)
                {
                    int lastIndexSpace = evt.LastIndexOf(" ");
                    string eventType = evt.Substring(0, lastIndexSpace);
                    string eventDuration = evt.Substring(lastIndexSpace + 1);
                    if (!activitiesToShow.ContainsKey(eventDuration))
                    {
                        activitiesToShow.Add(eventDuration, eventType);
                    }
                }

                var tableDay = new TableDay(activitiesToShow);
                tableDay.FillAndDraw();
            }
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

        private static bool UserExists(string username)
        {
            if (!DataBase.UserExists(username))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("User with username {0} does not exist", username));
                Console.ForegroundColor = ConsoleColor.Cyan;
                return false;
            }

            return true;
        }

        private static void ListAllWorkers()
        {
            var result = new StringBuilder();

            var allWorkers = new List<Worker>();

            var allRegulars = DataBase.GetAllRegularWorkers();
            var allteamLeaders = DataBase.GetAllTeamLeaders();
            allWorkers.AddRange(allRegulars);
            allWorkers.AddRange(allteamLeaders);

            result.AppendLine();
            foreach (var worker in allWorkers)
            {
                result.AppendLine(string.Format("User: {0}, Name: {1} {2}", worker.Username, worker.FirstName, worker.LastName, worker.UserType));
            }


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(result.ToString().Trim());
            Console.ForegroundColor = ConsoleColor.Cyan;

        }

    }
}