﻿using System;
using TimeManager.ViewModel;
using Xamarin.Forms;

namespace TimeManager
{
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
            ViewModel = (this.BindingContext as HomeViewModel);
        }

        public HomeViewModel ViewModel { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Init();
        }

        private void HistoryToday_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoryToday());
        }

        private void AllHistory_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AllHistory());
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.Suspend();
        }
    }
}
