using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamNectarineScheduleManager
{
    class PersonalCalendar : Calendar
    {
        private const int DaysLeave = 20; // this is how many days of paid leave every employee has
        private DateTime[] plannedLeave; // days with planned leave

        public PersonalCalendar(DateTime[] plannedLeave)
        {
            this.plannedLeave = plannedLeave;
        }
    }
}
