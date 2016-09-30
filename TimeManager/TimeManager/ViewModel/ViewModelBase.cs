using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeManager.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region EventHandlers

        public event PropertyChangedEventHandler PropertyChanged; 

        #endregion

        #region Methods

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 

        #endregion
    }
}
