using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void ComputeActiveText()
        {
            ActiveText = GetActiveText(isActive);
            InActiveText = GetActiveText(!isActive);
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

        public string GetActiveText(bool isActive)
        {
            return isActive ? "OUT" : "IN";
        }

        private void OnInOutCommand()
        {
            Session activeSession = AllSession.FirstOrDefault(s => s.BeginTime.Date == DateTime.Now.Date);
            if (activeSession == null)
            {
                activeSession = new Session(DateTime.Now);
                AllSession.Add(activeSession);
                IsActive = true;
            }
            else
            {
                IsActive = !IsActive;
            }
        }
    }
}
