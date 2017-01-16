namespace TeamNectarineScheduleManager.Calendars
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public class Calendar : DailyEvent, ICalendar
    {
        private IList<DailyEvent>[] day;

        public Calendar()
        {
            this.day = new List<DailyEvent>[366];
            List<DailyEvent> dailySchedule = new List<DailyEvent>();
            //day = new List<DailyEvents>[366];

            List<DailyEvent> daySchedule = new List<DailyEvent>();
            for (int i = 0; i < 366; i++)
            {
                day[i] = new List<DailyEvent>();
            }
        }

        public void AddEvent(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType type)
        {
            DailyEvent _event = new DailyEvent(dayOfTheMonth, month, year, eventStart, eventEnd, type);
            DateTime _eventDate = new DateTime(year, month, dayOfTheMonth);
           // _eventDate = DateTime.Parse(dayOfTheMonth + "/" + month + "/" + year);
            this.day[_eventDate.DayOfYear - 1].Add(_event);
        }

        public void RemoveEvent(int dayOfTheMonth, int month, int year, string eventStart, string eventEnd, EventType eventType)
        {

            DateTime _eventDate = new DateTime(year, month, dayOfTheMonth);
            int targetDay = _eventDate.DayOfYear - 1;
            var eventToSearch = new DailyEvent(dayOfTheMonth, month, year, eventStart, eventEnd, eventType);
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
