using System;
using System.Collections.Generic;
using System.Linq;
using TimeManager.Model;

namespace TimeManager.ViewModel
{
    public class Session : ViewModelBase
    {
        #region Fields

        private TimeInfo beginTime;
        private TimeInfo endTime;
        private string timeDiffText;
        private bool isActive;
        private float sessionPercentage;
        private TimeSpan sessionTime;

        #endregion

        #region Methods

        public Session()
        {
            ComputeSessionInfo();
        }

        public Session(DateTime dateTime)
        {
            TimeLine = new List<TimeInfo>();
            TimeLine.Add(new TimeInfo(dateTime, true));
            ComputeSessionInfo();
        }

        #endregion

        #region Properties

        public TimeInfo BeginTime
        {
            get
            {
                return beginTime;
            }
            set
            {
                beginTime = value;
                RaisePropertyChanged();
            }
        }

        public TimeInfo EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
                RaisePropertyChanged();
            }
        }

        public TimeInfo LastEntry { get; set; }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                RaisePropertyChanged();
            }
        }

        public string DayOfWeek
        {
            get
            {
                return BeginTime != null ? BeginTime.Time.DayOfWeek.ToString() : string.Empty;
            }
        }

        public TimeSpan SessionTime
        {
            get
            {
                return sessionTime;
            }
            set
            {
                sessionTime = value;
                TimeSpan divisionTime = new TimeSpan(8, 0, 0);
                SessionPercentage = ((SessionTime.Ticks * 100) / divisionTime.Ticks) ;
            }
        }

        public List<TimeInfo> TimeLine { get; set; }

        public string TimeDiffText
        {
            get
            {
                return timeDiffText;
            }
            set
            {
                timeDiffText = value;
                RaisePropertyChanged();
            }
        }

        public float SessionPercentage
        {
            get
            {
                return sessionPercentage;
            }
            set
            {
                if (value != sessionPercentage)
                {
                    sessionPercentage = value;
                    RaisePropertyChanged(); 
                }
            }
        }

        #endregion

        #region Methods

        public void AddToTimeLine(DateTime dateTime, bool isActive)
        {
            TimeLine.Add(new TimeInfo(dateTime, isActive));
            ComputeSessionInfo();
        }

        private void ComputeSessionInfo()
        {
            BeginTime = (TimeLine != null && TimeLine.Count > 0) ? TimeLine.FirstOrDefault() : null;
            var endTime = (TimeLine != null && TimeLine.Count > 0) ? TimeLine.LastOrDefault(s => !s.IsActive) : null;
            if (endTime != null)
            {
                EndTime = endTime;
            }

            if (BeginTime != null && EndTime != null)
            {
                TimeSpan timeDiff = TimeSpan.Zero;
                for (int i = 0; i < TimeLine.Count; i = i + 2)
                {
                    if (i + 1 < TimeLine.Count)
                    {
                        var start = TimeLine[i];
                        var end = TimeLine[i + 1];
                        if (start != null && end != null)
                        {
                            timeDiff += (end.Time - start.Time);
                        }
                    }
                }
                SessionTime = timeDiff;

                TimeDiffText = string.Format("{0}h:{1}m", timeDiff.Hours, timeDiff.Minutes);
            }
            LastEntry = TimeLine?.LastOrDefault();
            if (LastEntry != null)
            {
                IsActive = LastEntry.IsActive;
            }
        }

        #endregion
    }
}
