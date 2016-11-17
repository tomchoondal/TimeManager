using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeManager.Helpers;
using TimeManager.Model;

namespace TimeManager.ViewModel
{
    public class HomeViewModel : PageViewModel
    {
        #region Fields

        private Session lastSession;
        private bool isActive;
        private List<Session> allSession;
        private string activeText;
        private string inActiveText;
        private string inTimeText;
        private string outTimeText;
        private string timeDiffText;
        private string weekTotalTime;

        private ICommand inOutCommand;
        private ICommand navigateToHistoryToday;
        private ICommand navigateToAllHistory;

        #endregion

        #region Commands

        public ICommand InOutCommand
        {
            get
            {
                return inOutCommand ??
                  (inOutCommand = new RelayCommand(OnInOutCommand));
            }
        }

        public ICommand NavigateToHistoryToday
        {
            get
            {
                return navigateToHistoryToday ??
                  (navigateToHistoryToday = new RelayCommand(OnNavigateToHistoryToday));
            }
        }

        public ICommand NavigateToAllHistory
        {
            get
            {
                return navigateToAllHistory ??
                (navigateToAllHistory = new RelayCommand(OnNavigateToAllHistory));
            }
        }

        #endregion

        #region Properties

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

        public string WeekTotalTime
        {
            get
            {
                return weekTotalTime;
            }
            set
            {
                weekTotalTime = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Methods

        private void ComputeActiveText()
        {
            ActiveText = GetActiveText(isActive);
            InActiveText = GetActiveText(!isActive);

            ComputeLastSession();

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
                    var weeklyTime = new TimeSpan(AllSession.Sum(s => s.SessionTime.Ticks));
                    WeekTotalTime = string.Format("{0}h:{1}m", weeklyTime.Hours, weeklyTime.Minutes);

                }
            }
        }

        public string GetActiveText(bool isActive)
        {
            return isActive ? "OUT" : "IN";
        }

        private async void OnInOutCommand()
        {
            ComputeLastSession();
            if (LastSession == null)
            {
                LastSession = new Session(DateTime.Now);
                AllSession.Add(LastSession);
            }
            else
            {
                bool activeChange = !LastSession.IsActive;
                LastSession.AddToTimeLine(DateTime.Now, activeChange);
            }
            ComputeActiveState();
            await ContentDataProvider.Instance.SaveAllSessionsAsync(allSession);
        }

        private void ComputeActiveState()
        {
            ComputeLastSession();
            IsActive = LastSession != null ? LastSession.IsActive : false;
        }

        private void OnNavigateToHistoryToday()
        {
            App.NavigationService.Push(new HistoryToday());
        }

        private void OnNavigateToAllHistory()
        {
            App.NavigationService.Push(new AllHistory());
        }

        private void ComputeLastSession()
        {
            LastSession = AllSession.FirstOrDefault(s => s.BeginTime != null && s.BeginTime.Time.Date == DateTime.Now.Date);
           
        }

        public async override Task Init()
        {
            AllSession = await ContentDataProvider.Instance.GetAllSessionsAsync();
            ComputeActiveState();
        }

        #endregion
    }
}
