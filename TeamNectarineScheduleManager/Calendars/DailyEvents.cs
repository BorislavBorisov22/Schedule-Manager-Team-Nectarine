namespace TeamNectarineScheduleManager.Calendars
{
    using System;

    [Serializable]
    public class DailyEvents
    {
        private Events _event;
        private DateTime _eventStart;
        private DateTime _eventLenght;

        public DailyEvents()
        {

        }

        public DailyEvents(DateTime eventStart, DateTime eventLenght, Events currentEvent = Events.offDuty)
        {
            this._event = currentEvent;
            this._eventStart = eventStart;
            this._eventLenght = eventLenght;
        }
    }
}
