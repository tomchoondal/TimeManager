using TimeManager.ViewModel;
using Xamarin.Forms;

namespace TimeManager
{
    public partial class AllHistory : ContentPage
    {
        public AllHistory()
        {
            InitializeComponent();
            ViewModel = (this.BindingContext as AllHistoryViewModel);
        }

        public AllHistoryViewModel ViewModel { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Init();
        }
    }
}
