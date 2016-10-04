using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TimeManager
{
    public class App : Application
    {
        public static NavigationService NavigationService { get; set; }

        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new HomePage());
            NavigationService = new NavigationService(MainPage.Navigation);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }

    public class NavigationService
    {
        INavigation navigation;

        public NavigationService(INavigation nav)
        {
            navigation = nav;
        }

        public void Push(Page page)
        {
            navigation.PushAsync(page);
        }
    }
}
