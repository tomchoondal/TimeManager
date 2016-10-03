using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeManager.Helpers;
using TimeManager.Model;

namespace TimeManager.ViewModel
{
    public class HistoryTodayViewModel : PageViewModel
    {
        #region Fields

        private Session session;

        #endregion

        #region Properties

        public Session Session
        {
            get
            {
                return session;
            }
            set
            {
                session = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Methods

        public async override Task Init()
        {
            List<Session> allSessions = await ContentDataProvider.Instance.GetAllSessionsAsync();
            Session = allSessions.FirstOrDefault(s => s.BeginTime != null && s.BeginTime.Time.Date == DateTime.Now.Date);
        }

        #endregion
    }
}
