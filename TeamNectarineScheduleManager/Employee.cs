using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamNectarineScheduleManager
{
    public class Employee : User
    {

        private PersonalCalendar employeeCalendar;
        private CompanyCalendar companyCalendar;

        public Employee()
        {
            //this.username = "username1234"; // default username & password
            //this.password = "password1234";
        }

        public Employee(string username, string password, PersonalCalendar employeeCalendar, CompanyCalendar companyCalendar)
        {
            //this.username = username;
            //this.password = password;
            this.employeeCalendar = employeeCalendar;
            this.companyCalendar = companyCalendar;
        }
    }
}
