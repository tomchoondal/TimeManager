using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TimeManager.Helpers;
using TimeManager.Model;

namespace TimeManager.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private Session lastSession;
        private bool isActive;
        private List<Session> allSession;
        private string activeText;
        private string inActiveText;
        private string inTimeText;
        private string outTimeText;
        private string timeDiffText;

        private ICommand inOutCommand;

        public HomeViewModel()
        {
            AllSession = new List<Session>();
            ComputeActiveText();
        }


        public ICommand InOutCommand
        {
            get
            {
                return inOutCommand ??
                  (inOutCommand = new RelayCommand(OnInOutCommand));
            }
        }

        public List<Session> AllSession
        {
            get { return allSession; }
            set
            {
                allSession = value;
                RaisePropertyChanged();
            }
        }

        public Session LastSession
        {
            get
            {
                return lastSession;
            }
            set
            {
                lastSession = value;
                RaisePropertyChanged();
            }
        }

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
                ComputeActiveText();
            }
        }

        public string ActiveText
        {
            get
            {
                return activeText;
            }
            set
            {
                activeText = value;
                RaisePropertyChanged();
            }
        }

        public string InActiveText
        {
            get
            {
                return inActiveText;
            }
            set
            {
                inActiveText = value;
                RaisePropertyChanged();
            }
        }

        public string OutTimeText
        {
            get
            {
                return outTimeText;
            }
            set
            {
                outTimeText = value;
                RaisePropertyChanged();
            }
        }

        public string InTimeText
        {
            get
            {
                return inTimeText;
            }
            set
            {
                inTimeText = value;
                RaisePropertyChanged();
            }
        }

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

        private void ComputeActiveText()
        {
            ActiveText = GetActiveText(isActive);
            InActiveText = GetActiveText(!isActive);

            if (LastSession != null)
            {
                var beginTime = LastSession.BeginTime;
                if (beginTime != null && beginTime.IsActive)
                {
                    InTimeText = string.Format("{0:t}", beginTime.Time);
                }

                var endTime = LastSession.EndTime;
                if (endTime != null && !endTime.IsActive)
                {
                    OutTimeText = string.Format("{0:t}", endTime.Time);
                }

                if (beginTime != null && endTime != null)
                {
                    TimeSpan timeDiff = endTime.Time - beginTime.Time;
                    TimeDiffText = string.Format("{0}h:{1}m", timeDiff.Hours, timeDiff.Minutes);
                }
            }
        }

        public string GetActiveText(bool isActive)
        {
            return isActive ? "OUT" : "IN";
        }

        private void OnInOutCommand()
        {
            LastSession = AllSession.FirstOrDefault(s => s.BeginTime != null && s.BeginTime.Time.Date == DateTime.Now.Date);
            if (LastSession == null)
            {
                LastSession = new Session(DateTime.Now);
                AllSession.Add(LastSession);
                IsActive = true;
            }
            else
            {
                bool activeChange = !IsActive;
                LastSession.AddToTimeLine(DateTime.Now, activeChange);
                IsActive = activeChange;
            }
        }
    }
}
