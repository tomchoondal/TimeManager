using System;

namespace TimeManager.Model
{
    public class TimeInfo
    {
        #region Constructors

        public TimeInfo(DateTime dateTime, bool isActive)
        {
            this.Time = dateTime;
            this.IsActive = isActive;
        }

        #endregion

        #region Properties

        public bool IsActive { get; set; }

        public DateTime Time { get; set; } 

        #endregion
    }
}
