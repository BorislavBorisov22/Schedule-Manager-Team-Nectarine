namespace TeamNectarineScheduleManager.Calendars
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public class Calendar : DailyEvent, ICalendar
    {
        private IList<DailyEvent>[] daysOfYear;

        public Calendar()
        {
            this.daysOfYear = new List<DailyEvent>[366];
            var dailySchedule = new List<DailyEvent>();

            for (int i = 0; i < daysOfYear.Length; i++)
            {
                daysOfYear[i] = new List<DailyEvent>();
            }
        }

        public void AddEvent(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType type)
        {
            var dailyEvent = new DailyEvent(dayOfTheMonth, month, year, eventStart, eventEnd, type);
            var eventDate = new DateTime(year, month, dayOfTheMonth);
            // _eventDate = DateTime.Parse(dayOfTheMonth + "/" + month + "/" + year);
            this.daysOfYear[eventDate.DayOfYear - 1].Add(dailyEvent);
        }

        public void RemoveEvent(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType eventType)
        {

            DateTime _eventDate = new DateTime(year, month, dayOfTheMonth);
            int targetDay = _eventDate.DayOfYear - 1;
            var eventToSearch = new DailyEvent(dayOfTheMonth, month, year, eventStart, eventEnd, eventType);
            int targetIndex = -1;

            for (int i = 0; i < this.daysOfYear[targetDay].Count; ++i)
            {
                bool isMatch = this.daysOfYear[targetDay][i].Event == eventToSearch.Event &&
                    this.daysOfYear[targetDay][i].EventStart == eventToSearch.EventStart &&
                    this.daysOfYear[targetDay][i].EventEnd == eventToSearch.EventEnd;

                if (isMatch)
                {
                    targetIndex = i;
                    break;
                }
            }

            if (targetIndex != -1)
            {
                this.daysOfYear[targetDay].RemoveAt(targetIndex);
            }
            //_eventDate = DateTime.Parse(dayOfTheMonth + "/" + month + "/" + year);

            //this.day[_eventDate.DayOfYear - 1].RemoveAt(eventNumber);
        }

        public string[] ToString(int dayOfTheMonth, int month, int year)
        {
            var eventDate = new DateTime(year, month, dayOfTheMonth);
            //_eventDate = DateTime.Parse(dayOfTheMonth + "/" + month + "/" + year);

            int numberOfEvents = this.daysOfYear[eventDate.DayOfYear - 1].Count;
            string[] result = new string[numberOfEvents];

            for (int i = 0; i < numberOfEvents; i++)
            {
                result[i] = "EventNumber[" + i + "] " + this.daysOfYear[eventDate.DayOfYear - 1][i].ToString();
            }

            return result;
        }
    }
}
