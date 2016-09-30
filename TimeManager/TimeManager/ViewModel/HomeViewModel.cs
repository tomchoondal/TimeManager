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
        private FileManager fileManager;

        private ICommand inOutCommand;

        #endregion

        #region Constructors

        public HomeViewModel()
        {
            fileManager = new FileManager("content.json");
        }

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
                }
            }
        }

        public string GetActiveText(bool isActive)
        {
            return isActive ? "OUT" : "IN";
        }

        private void OnInOutCommand()
        {
            ComputeLastSession();
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
            SaveAllSessionsAsync();
        }

        private void ComputeLastSession()
        {
            LastSession = AllSession.FirstOrDefault(s => s.BeginTime != null && s.BeginTime.Time.Date == DateTime.Now.Date);
        }

        public async override Task Init()
        {
            AllSession = await GetAllSessionsAsync();
            ComputeActiveText();
        }

        private async Task<List<Session>> GetAllSessionsAsync()
        {
            List<Session> session = null;
            string content = await fileManager.GetTextAsync();
            if (content != null)
            {
                try
                {
                    session = JsonConvert.DeserializeObject<List<Session>>(content);
                }
                catch (Exception)
                {
                }
            }
            if (session == null)
            {
                session = new List<Session>();
            }
            return session;
        }

        private async void SaveAllSessionsAsync()
        {
            if (AllSession != null)
            {
                string content = JsonConvert.SerializeObject(AllSession);
                await fileManager.SetTextAsync(content);
            }
        }

        #endregion
    }
}
