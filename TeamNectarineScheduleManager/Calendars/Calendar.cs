namespace TeamNectarineScheduleManager.Calendars
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Calendar : DailyEvents, ICalendar
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
            DateTime _eventDate = new DateTime(year, month, dayOfTheMonth);
           // _eventDate = DateTime.Parse(dayOfTheMonth + "/" + month + "/" + year);
            this.day[_eventDate.DayOfYear - 1].Add(_event);
        }

        public void RemoveEvent(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType eventType)
        {

            DateTime _eventDate = new DateTime(year, month, dayOfTheMonth);
            int targetDay = _eventDate.DayOfYear - 1;
            var eventToSearch = new DailyEvents(dayOfTheMonth, month, year, eventStart, eventEnd, eventType);
            int targetIndex = -1;

            for (int i = 0; i < this.day[targetDay].Count; ++i)
            {
                bool isMatch = this.day[targetDay][i].Event == eventToSearch.Event &&
                    this.day[targetDay][i].EventStart == eventToSearch.EventStart &&
                    this.day[targetDay][i].EventEnd == eventToSearch.EventEnd;

                if (isMatch)
                {
                    targetIndex = i;
                    break;
                }
            }

            if (targetIndex != -1)
            {
                this.day[targetDay].RemoveAt(targetIndex);
            }
            //_eventDate = DateTime.Parse(dayOfTheMonth + "/" + month + "/" + year);
           
            //this.day[_eventDate.DayOfYear - 1].RemoveAt(eventNumber);
        }

        public string[] ToString(int dayOfTheMonth, int month, int year)
        {
            DateTime _eventDate = new DateTime(year, month, dayOfTheMonth);
            //_eventDate = DateTime.Parse(dayOfTheMonth + "/" + month + "/" + year);

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
