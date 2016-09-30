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
        private FileManager fileManager;

        #endregion

        #region Constructors

        public HistoryTodayViewModel()
        {
            fileManager = new FileManager("content.json");
        }

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
            List<Session> allSessions = await GetAllSessionsAsync();
            Session = allSessions.FirstOrDefault(s => s.BeginTime != null && s.BeginTime.Time.Date == DateTime.Now.Date);
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

        #endregion
    }
}
