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
            
        }

        public void AddEvent(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType eventType)
        {
            DailyEvents _event = new DailyEvents(dayOfTheMonth, month, year, eventStart, eventEnd, eventType);
            DateTime _eventDate = new DateTime();
            _eventDate = DateTime.Parse(dayOfTheMonth + "/" + month + "/" + year);
            this.personalCalendar[_eventDate.DayOfYear - 1].Add(_event);
        }

        public void RemoveEvent(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, int EventNumber)
        {
            DateTime _eventDate = new DateTime();
            _eventDate = DateTime.Parse(dayOfTheMonth + "/" + month + "/" + year);
            this.personalCalendar[_eventDate.DayOfYear - 1].RemoveAt(EventNumber);
        }
        public string[] ToString(int dayOfTheMonth, int month, int year)
        {
            DateTime _eventDate = new DateTime();
            _eventDate = DateTime.Parse(dayOfTheMonth + "/" + month + "/" + year);

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
