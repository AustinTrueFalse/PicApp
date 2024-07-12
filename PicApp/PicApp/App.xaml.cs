using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PicApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            bool isFirstLaunch = !Application.Current.Properties.ContainsKey("UserPIN");
            MainPage = new NavigationPage(new PinCodePage(isFirstLaunch));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
