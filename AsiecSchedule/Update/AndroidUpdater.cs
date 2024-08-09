#if ANDROID

using AsiecSchedule.Data;

namespace AsiecSchedule.Update
{
    public class AndroidUpdater : IUpdater
    {
        public void Update(string sourcePath)
        {
            Android.Content.PM.PackageManager? packageManager = Platform.CurrentActivity.PackageManager;
            if (!packageManager.CanRequestPackageInstalls()) return;

            Android.Content.Context context = Android.App.Application.Context;
            Java.IO.File file = new(sourcePath);

            using (Android.Content.Intent install = new(Android.Content.Intent.ActionView))
            {
                Android.Net.Uri apkURI = AndroidX.Core.Content.FileProvider.GetUriForFile(context, context.ApplicationContext.PackageName + ".provider", file);
                install.SetDataAndType(apkURI, "application/vnd.android.package-archive");
                install.AddFlags(Android.Content.ActivityFlags.NewTask);
                install.AddFlags(Android.Content.ActivityFlags.GrantReadUriPermission);
                install.AddFlags(Android.Content.ActivityFlags.ClearTop);
                install.PutExtra(Android.Content.Intent.ExtraNotUnknownSource, true);

                Platform.CurrentActivity.StartActivity(install);
            }
        }

        public void FreeTempResources(string sourcePath)
        {
            File.Delete(sourcePath);
        }

        public bool CheckPermissions()
        {
            Android.Content.PM.PackageManager? packageManager = Platform.CurrentActivity.PackageManager;

            if (packageManager.CanRequestPackageInstalls()) return true;

            Platform.CurrentActivity.StartActivityForResult(
                new Android.Content.Intent(Android.Provider.Settings.ActionManageUnknownAppSources, Android.Net.Uri.Parse("package:" + Microsoft.Maui.ApplicationModel.AppInfo.Current.PackageName)),
                (int)ActivityRequestCode.CanRequestPackageInstalls);

            return packageManager.CanRequestPackageInstalls();
        }
    }
}

#endif