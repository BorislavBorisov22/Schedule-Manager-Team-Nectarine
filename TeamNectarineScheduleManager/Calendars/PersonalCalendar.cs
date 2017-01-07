namespace TeamNectarineScheduleManager.Calendars
{
    using System;

    [Serializable]
    public class PersonalCalendar : Calendar
    {
        private const int DaysLeave = 20; // this is how many days of paid leave every employee has
        private DateTime[] plannedLeave; // days with planned leave

        public PersonalCalendar()
        {

        }

        public PersonalCalendar(DateTime[] plannedLeave)
        {
            this.plannedLeave = plannedLeave;
        }

        public PersonalCalendar(Month month, int week, DayOfWeek day)
            : base(month, week, day)
        {
        }
    }
}
