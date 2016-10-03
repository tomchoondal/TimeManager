using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.ViewModel;
using Xamarin.Forms;

namespace TimeManager
{
    public partial class HistoryToday : ContentPage
    {
        public HistoryToday()
        {
            InitializeComponent();
            ViewModel = (this.BindingContext as HistoryTodayViewModel);
        }

        public HistoryTodayViewModel ViewModel { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Init();
        }
    }
}
