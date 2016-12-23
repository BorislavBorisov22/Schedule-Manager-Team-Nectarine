using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamNectarineScheduleManager
{
    class Employee
    {
        private string username; // needed for log-in
        private string password; // needed for log-in
        private EmployeeCalendar employeeCalendar;
        private static CompanyCalendar companyCalendar = CompanyCalendar();

        public Employee()
        {
            this.username = "username1234"; // default username & password
            this.password = "password1234";
            this.employeeCalendar = new EmployeeCalendar();
        }

        public Employee(string username, string password, EmployeeCalendar employeeCalendar)
        {
            this.username = username;
            this.password = password;
            this.employeeCalendar = employeeCalendar;
        }
    }
}
