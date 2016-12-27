using System;
using System.Collections.Generic;
using System.Linq;
namespace TeamNectarineScheduleManager
{
    using Users;

    public class Start
    {
        public static void Main()
        {
            Test_Workers_And_Admins_Instances();
        }

        public static void Test_Workers_And_Admins_Instances()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Administrator("Vankata_123", "089393929aa", "Ivan", "Ivanov"),
                new RegularWorker("DeqnStanisl","sexy_pich12549","Deqn","Stanislavov"),
                new TeamLeaderWorker("Peshotron2000","1234322Abcde","Petar", "Petrov")
            };

            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
                Console.WriteLine("==================");
            }
            
        }
    }
}
