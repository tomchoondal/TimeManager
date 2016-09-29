using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeManager.Model
{
    public class Session
    {
        public Session() { }

        public Session(DateTime dateTime)
        {
            TimeLine = new List<TimeInfo>();
            TimeLine.Add(new TimeInfo(dateTime, true));
        }

        public TimeInfo BeginTime
        {
            get
            {
                return (TimeLine != null && TimeLine.Count > 0) ? TimeLine.FirstOrDefault() : null;
            }
        }

        public TimeInfo EndTime
        {
            get
            {
                return (TimeLine != null && TimeLine.Count > 0) ? TimeLine.LastOrDefault() : null;
            }
        }

        public void AddToTimeLine(DateTime dateTime, bool isActive)
        {
            TimeLine.Add(new TimeInfo(dateTime, isActive));
        }

        public List<TimeInfo> TimeLine { get; set; }
    }

    public class TimeInfo
    {
        public TimeInfo(DateTime dateTime, bool isActive)
        {
            this.Time = dateTime;
            this.IsActive = isActive;
        }

        public bool IsActive { get; set; }

        public DateTime Time { get; set; }
    }
}
