namespace TeamNectarineScheduleManager.Calendars
{
    using System;
    using System.Collections.Generic;

    public class Calendar : DailyEvents
    {
        public List<DailyEvents>[] day;
        private static readonly DateTime[] officialHolidays; // initialize with dates of official holidays
        public Calendar()
        {

            day = new List<DailyEvents>[366];
            List<DailyEvents> dailySchedule = new List<DailyEvents>();
            day = new List<DailyEvents>[366];

            List<DailyEvents> daySchedule = new List<DailyEvents>();
            for (int i = 0; i < 366; i++)
            {
                day[i] = new List<DailyEvents>();
            }
        }

        public void AddEvent(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType evt)
        {
            DailyEvents _event = new DailyEvents(dayOfTheMonth, month, year, eventStart, eventEnd, evt);
            DateTime _eventDate = new DateTime();
            _eventDate = DateTime.Parse(dayOfTheMonth + "/" + month + "/" + year);
            this.day[_eventDate.DayOfYear - 1].Add(_event);
        }

        public void RemoveEvent(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, int eventNumber)
        {
            DateTime _eventDate = new DateTime();
            _eventDate = DateTime.Parse(dayOfTheMonth + "/" + month + "/" + year);
            this.day[_eventDate.DayOfYear - 1].RemoveAt(eventNumber);
        }
        public string[] ToString(int dayOfTheMonth, int month, int year)
        {
            DateTime _eventDate = new DateTime();
            _eventDate = DateTime.Parse(dayOfTheMonth + "/" + month + "/" + year);

            int numberOfEvents = this.day[_eventDate.DayOfYear - 1].Count;
            string[] result = new string[numberOfEvents];
            for (int i = 0; i < numberOfEvents; i++)
            {
                result[i] = "EventNumber[" + i + "] " + this.day[_eventDate.DayOfYear - 1][i].ToString();
            }
            return result;
        }
    }
}
