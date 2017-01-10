namespace TeamNectarineScheduleManager
{
    using System;
    using System.Collections.Generic;

    using Users;
    using DataBaseLibrary;
    using UserInterface;
    using Calendars;
    using Teams;

    public class Start
    {
        public static void Main()
        {
            //TestDataBase();
            UI.ShowMainMenu();
            // Test Team class
            //Test_Team_Class();
            //Test_Workers_And_Admins_Instances();
        }

        public static void Test_Team_Class()
        {
            var teamLeader = new TeamLeaderWorker("IvanIvanov1", "0875123a", "Ivan", "Ivanov");
            var teamMemeber = new RegularWorker("PepoPepov22", "14234123a", "Petar", "Petrov");
            var anotherTeamMember = new RegularWorker("Vancheto_петрова", "gjk654322232.", "Ivanka", "Vancheva");
            var team = new Team("Nectarine", teamLeader);
            team.AddMember(teamMemeber);
            team.AddMember(anotherTeamMember);
            foreach (var member in team.Members)
            {
                Console.WriteLine(member);
                Console.WriteLine("--------");
            }

            var vankataLeader = new TeamLeaderWorker("Gosho", "goshumbata1234324", "Georgi", "Georgiev");
            var vankataTeam = new Team("Nectarine", vankataLeader);
            var regularWorker = new RegularWorker("pepito54321", "13241fdasf", "Petar", "Petrov");
            team.AddMember(regularWorker);
            Console.WriteLine(regularWorker.Team);
            team.RemoveMemeber(regularWorker);
            Console.WriteLine(regularWorker.Team);
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
            TeamLeaderWorker peter = new TeamLeaderWorker("Peter", "CertifiedIdiot12345", "Peter", "Griffin");
            RegularWorker lois = new RegularWorker("Loiss", "HouseWife12345", "Lois", "Griffin");
            RegularWorker chris = new RegularWorker("Chris", "BaseballCap12345", "Chris", "Griffin");
            RegularWorker brian = new RegularWorker("Brian", "TalkingDoggo12345", "Brian", "Griffin");

            Team theGriffins = new Team("The Griffins", peter);
            theGriffins.AddMember(lois);
            theGriffins.AddMember(chris);
            theGriffins.AddMember(brian);

            DataBase.Save(peter);
            DataBase.Save(lois);
            DataBase.Save(chris);
            DataBase.Save(brian);
            DataBase.Save(theGriffins);

            TeamLeaderWorker peterReloaded = DataBase.LoadTeamLeader(peter.Username);
            Console.WriteLine($"{peterReloaded.Username} {peterReloaded.Password} {peterReloaded.FirstName } {peterReloaded.LastName}");
            //for (int i = 0; i < peterReloaded.Team.MembersCount; i++)
            //{
            //    Console.WriteLine($"{peterReloaded.Team.Members[i].Username} {peterReloaded.Team.Members[i].Password} {peterReloaded.Team.Members[i].FirstName} {peterReloaded.Team.Members[i].LastName} ");
            //}
        }
    }
}
