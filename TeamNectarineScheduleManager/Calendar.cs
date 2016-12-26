using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamNectarineScheduleManager
{
    public enum DayOfWeek
    {
        Monday = 1,
        Tuesday,
        Wednsday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    class Calendar : DailyEvents
    {
        private Month _month;
        private int _week; // specify week number, must be between 1 and 53 inclusive
        private DayOfWeek _day;
        public List<DailyEvents> _employeeDailyEvents;//Creates list of with events for the current calendar.
        private static readonly DateTime[] officialHolidays; // initialize with dates of official holidays

        public Calendar() { }
        public Calendar(Month month, int week, DayOfWeek day)
        {
            this._month = month;
            this._day = day;
            this._week = week;
            _employeeDailyEvents = new List<DailyEvents>();
        }
        public List<DailyEvents> CalendarEvents
        {
            get
            {
                return _employeeDailyEvents;
            }
            set
            {
                _employeeDailyEvents = value;
            }
        }
        public List<DailyEvents> AddEvent(DailyEvents currentEvent, Calendar calendar)
        {
            calendar.CalendarEvents.Add(currentEvent);
            return CalendarEvents;
        }//Adds event for the current caledar
        public List<DailyEvents> RemoveEvent(int removeIndex, Calendar calendar)
        {
            calendar.CalendarEvents.RemoveAt(removeIndex);
            return CalendarEvents;
        }//Removes event from the current calendar
    }

}

