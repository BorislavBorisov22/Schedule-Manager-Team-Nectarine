namespace TeamNectarineScheduleManager.Calendars
{
    using System;
    using System.Collections.Generic;

    public class Calendar : DailyEvents
    {
        public List<DailyEvents>[] personalCalendar;
        private static readonly DateTime[] officialHolidays; // initialize with dates of official holidays
        public Calendar()
        {

            personalCalendar = new List<DailyEvents>[366];

            List<DailyEvents> daySchedule = new List<DailyEvents>();
            for (int i = 0; i < 366; i++)
            {
                personalCalendar[i] = new List<DailyEvents>();
            }
            Console.WriteLine(personalCalendar.Count());
            Console.WriteLine(personalCalendar.ToString());
        }

        public void AddEvent(int DayOfTheMonth, int Month, int Year, string EventStart, string EventEnd, Events Event)
        {
            DailyEvents _event = new DailyEvents(DayOfTheMonth, Month, Year, EventStart, EventEnd, Event);
            DateTime _eventDate = new DateTime();
            _eventDate = DateTime.Parse(DayOfTheMonth + "/" + Month + "/" + Year);
            this.personalCalendar[_eventDate.DayOfYear - 1].Add(_event);
        }

        public void RemoveEvent(int DayOfTheMonth, int Month, int Year, string EventStart, string EventEnd, int EventNumber)
        {
            DateTime _eventDate = new DateTime();
            _eventDate = DateTime.Parse(DayOfTheMonth + "/" + Month + "/" + Year);
            this.personalCalendar[_eventDate.DayOfYear - 1].RemoveAt(EventNumber);
        }
        public string[] ToString(int DayOfTheMonth, int Month, int Year)
        {
            DateTime _eventDate = new DateTime();
            _eventDate = DateTime.Parse(DayOfTheMonth + "/" + Month + "/" + Year);

            int numberOfEvents = this.personalCalendar[_eventDate.DayOfYear - 1].Count;
            string[] result = new string[numberOfEvents];
            for (int i = 0; i < numberOfEvents; i++)
            {
                result[i] = "EventNumber[" + i + "] " + this.personalCalendar[_eventDate.DayOfYear - 1][i].ToString();
            }
            return result;
        }
    }
}
