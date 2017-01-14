using System;

namespace TeamNectarineScheduleManager.Calendars
{
    [Serializable]
    public class CompanyCalendar : Calendar
    {
        private const int DaysInAWeek = 7;
        private readonly int numberOfWorkingDays; // number of working days in the week
        private int workHoursDay;  // how many work hours all employees should achieve for the company
        private int workHoursWeek;

        public CompanyCalendar(int workHoursDay = 200)
        {
            this.WorkHoursDay = workHoursDay; // assume the company needs at least 200 working hours every day
        }

        public int WorkHoursDay
        {
            get
            {
                return this.workHoursDay;
            }

            private set
            {
                if (workHoursDay <= 0)
                {
                    throw new ArgumentOutOfRangeException("Company's total working hours a day cannot be zero or less");
                }

                this.workHoursDay = value;
            }
        }

        public int WorkHoursWeek
        {
            get
            {
                return this.workHoursDay * CompanyCalendar.DaysInAWeek;
            }
        }
    }
}
