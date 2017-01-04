namespace TeamNectarineScheduleManager.Calendars
{
    using System;

    [Serializable]
    public class DailyEvent
    {
        public DailyEvent()
        {

        }

        public DailyEvent(DateTime eventStart, DateTime eventEnd, EventType eventType = EventType.offDuty)
        {
            this.EventType = eventType;
            this.EventStart = eventStart;
            this.EventEnd = eventEnd;
        }

        public EventType EventType { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
    }
}
