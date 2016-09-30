using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeManager.Model
{
    public class Session
    {
        #region Methods

        public Session() { }

        public Session(DateTime dateTime)
        {
            TimeLine = new List<TimeInfo>();
            TimeLine.Add(new TimeInfo(dateTime, true));
        } 

        #endregion

        #region Properties

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

        public List<TimeInfo> TimeLine { get; set; }

        #endregion

        #region Methods

        public void AddToTimeLine(DateTime dateTime, bool isActive)
        {
            TimeLine.Add(new TimeInfo(dateTime, isActive));
        } 

        #endregion
    }
}
