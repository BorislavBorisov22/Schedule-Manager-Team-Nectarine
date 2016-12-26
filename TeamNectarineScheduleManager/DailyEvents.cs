using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamNectarineScheduleManager
{
    public enum Events
    {
        work = 1,
        work_break,
        offDuty,
        absent,
        holiday,
        sick
    }
    class DailyEvents
    {

        private Events _event;
        private DateTime _eventStart;
        private DateTime _eventLenght;
        public DailyEvents(Events currentEvent = Events.offDuty, DateTime eventStart, DateTime eventLenght)
        {
            this._event = currentEvent;
            this._eventStart = eventStart;
            this._eventLenght = eventLenght;
        }
        public DailyEvents()
        {
        }
    }
}
