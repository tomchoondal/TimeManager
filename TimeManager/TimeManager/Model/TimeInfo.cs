﻿using System;

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

        public string ActiveText
        {
            get
            {
                return IsActive ? "In" : "Out";
            }
        }

        public string TimeText
        {
            get
            {
                return Time.ToString("hh:mm:tt");
            }
        }

        public string TimeDayText
        {
            get
            {
                return Time.DayOfWeek.ToString();
            }
        }

        #endregion
    }
}
