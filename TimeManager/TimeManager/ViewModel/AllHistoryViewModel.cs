using System.Collections.Generic;
using System.Threading.Tasks;
using TimeManager.Model;

namespace TimeManager.ViewModel
{
    public class AllHistoryViewModel : PageViewModel
    {
        #region Fields

        private List<Session> allSessions;

        #endregion
        
        #region Properties

        public List<Session> AllSessions
        {
            get { return allSessions; }
            set
            {
                allSessions = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Methods

        public async override Task Init()
        {
            AllSessions = await ContentDataProvider.Instance.GetAllSessionsAsync();
        } 

        #endregion
    }
}
