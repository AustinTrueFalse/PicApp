using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PicApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PinCodePage : ContentPage
    {
        private bool isFirstLaunch;

        public PinCodePage(bool isFirstLaunch)
        {
            InitializeComponent();
            this.isFirstLaunch = isFirstLaunch;

            if (isFirstLaunch)
            {
                pinEntry.Placeholder = "Set PIN";
            }
            else
            {
                pinEntry.Placeholder = "Enter PIN";
            }
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            string pin = pinEntry.Text;

            if (string.IsNullOrEmpty(pin) || pin.Length != 4)
            {
                await DisplayAlert("Error", "PIN must be 4 digits", "OK");
                return;
            }

            if (isFirstLaunch)
            {
                Application.Current.Properties["UserPIN"] = pin;
                await Application.Current.SavePropertiesAsync();
                await Navigation.PushAsync(new GalleryPage());
            }
            else
            {
                if (Application.Current.Properties.ContainsKey("UserPIN") &&
                    Application.Current.Properties["UserPIN"].ToString() == pin)
                {
                    await Navigation.PushAsync(new GalleryPage());
                }
                else
                {
                    await DisplayAlert("Error", "Invalid PIN", "OK");
                }
            }
        }
    }
}