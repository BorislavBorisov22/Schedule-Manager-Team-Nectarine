namespace TeamNectarineScheduleManager.Calendars
{
    class CompanyCalendar : Calendar
    {
        private const int numberOfDays = 5; // number of working days in the week
        private int workHoursDay;  // how many work hours all employees should achieve for the company
        private int workHoursWeek;

        public CompanyCalendar(int workHoursDay, int workHoursMonth, int workHoursYear)
        {
            this.workHoursDay = 200; // assume the company needs at least 200 working hours every day
            this.workHoursWeek = numberOfDays * workHoursDay;
        }
    }
}
