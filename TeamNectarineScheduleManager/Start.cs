namespace TeamNectarineScheduleManager
{
    using System;
    using System.Collections.Generic;
    using Users;
    using DataBaseLibrary;
    using UserInterface;
    using DataBaseLibrary;
    using Calendars;

    public class Start
    {
        public static void Main()
        {
            //var peshoWorker = new Worker("pesho555", "910011abc", "Pesho", "Stamatov");
            //var peshoCalendar = new PersonalCalendar();
            //peshoWorker.PersonCalendar = peshoCalendar;
            //DataBase.WriteToDisc(peshoWorker);

            //Worker pesho2 = DataBase.ReadFromDiscWorker("pesho555");
            //Console.WriteLine(pesho2.Username);
            //Console.WriteLine(pesho2.Password);
            //Console.WriteLine(pesho2.FirstName);
            //Console.WriteLine(pesho2.LastName);

            UI.ShowMainMenu();
            // Test Team class
            //Test_Team_Class();
            // Test_Workers_And_Admins_Instances();
        }

        public static void Test_Team_Class()
        {
            var teamLeader = new TeamLeaderWorker("IvanIvanov1", "0875123a", "Ivan", "Ivanov");
            var teamMemeber = new RegularWorker("PepoPepov22", "14234123a", "Petar", "Petrov");
            var anotherTeamMember = new RegularWorker("Vancheto_sladurancheto", "gjk654322232.", "Ivanka", "Vancheva");
            var team = new Team("Nectarine", teamLeader);
            team.AddMember(teamMemeber);
            team.AddMember(anotherTeamMember);
            foreach (var member in team.Members)
            {
                Console.WriteLine(member);
                Console.WriteLine("--------");
            }
        }

        public static void Test_Workers_And_Admins_Instances()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Administrator("Vankata_123", "089393929aa", "Ivan", "Ivanov"),
                new RegularWorker("DeqnStanisl", "sexy_pich12549", "Deqn", "Stanislavov"),
                new TeamLeaderWorker("Peshotron2000", "1234322Abcde", "Petar", "Petrov")
            };

            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
                Console.WriteLine("==================");
            }
        }

        public static void TestDataBase()
        {
            Worker w = new Worker("Peter", "SpoonerStr00031", "Peter", "Griffin");
            DataBase.WriteToDisc(w);
            Worker w2 = DataBase.ReadFromDiscWorker("Peter");
            Console.WriteLine($"{w2.FirstName} {w2.LastName}");
        }
    }
}
