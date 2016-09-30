using System.Threading.Tasks;

namespace TimeManager.ViewModel
{
    public class PageViewModel : ViewModelBase
    {
        #region Methods

        public virtual Task Init() { return null; }

        public virtual Task Suspend() { return null; } 

        #endregion
    }
}
