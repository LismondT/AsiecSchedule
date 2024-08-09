using Android.App;
using Android.Content;
using Android.Content.PM;
using AsiecSchedule.Data;
using AsiecSchedule.Popups;
using AsiecSchedule.Update;
using CommunityToolkit.Maui.Views;

namespace AsiecSchedule
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == (int)ActivityRequestCode.CanRequestPackageInstalls)
            {
                if (resultCode == Result.Ok)
                {
                    AppGlobals.OnPermissionsSuccess?.Invoke();
                }
                else
                {
                    AppGlobals.OnPermissionsDenied?.Invoke();
                }
            }
        }

    }
}
