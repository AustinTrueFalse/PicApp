using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Xamarin.Essentials;
using Xamarin.Forms.Platform.Android;

namespace PicApp.Droid
{
    [Activity(Label = "PicApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : FormsAppCompatActivity
    {
        const int RequestStorageId = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            RequestStoragePermissions();
            LoadApplication(new App());
        }

        void RequestStoragePermissions()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                if (CheckSelfPermission(Manifest.Permission.ReadExternalStorage) != Permission.Granted)
                {
                    RequestPermissions(new[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage }, RequestStorageId);
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == RequestStorageId)
            {
                if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                {
                    // Permission granted
                }
                else
                {
                    // Permission denied
                    Android.Widget.Toast.MakeText(this, "Storage permission is required to access photos.", Android.Widget.ToastLength.Long).Show();
                }
            }
        }
    }
}