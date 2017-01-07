namespace TeamNectarineScheduleManager.Calendars
{
    using System;
    using System.Collections.Generic;

    [Serializable]

    public class Calendar
    {
        private Month _month;
        private int _week; // specify week number, must be between 1 and 53 inclusive
        private DayOfWeek _day;
        public List<DailyEvent> _employeeDailyEvents;//Creates list of with events for the current calendar.
        private static readonly DateTime[] officialHolidays; // initialize with dates of official holidays

        public Calendar()
        {

        }

        public Calendar(Month month, int week, DayOfWeek day)
        {
            this._month = month;
            this._day = day;
            this._week = week;
            _employeeDailyEvents = new List<DailyEvent>();
        }

        public List<DailyEvent> CalendarEvents
        {
            get
            {
                return new List<DailyEvent>(this._employeeDailyEvents);
            }
        }

        // Adds event for the current calendar
        public void AddEvent(DailyEvent currentEvent)
        {
            if (currentEvent == null)
            {
                throw new ArgumentNullException("DailyEvent with a null value cannot be added to calendar");
            }

            this._employeeDailyEvents.Add(currentEvent);
        }
        // Removes event from the current calendar
        public void RemoveEvent(int removeIndex, Calendar calendar)
        {
            calendar.CalendarEvents.RemoveAt(removeIndex);
        }
    }
}
