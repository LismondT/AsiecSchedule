#if ANDROID

using AsiecSchedule.Data;

namespace AsiecSchedule.Update
{
    public class AndroidUpdater : IUpdater
    {
        public async Task Update(string url)
        {
            var packageManager = Platform.CurrentActivity.PackageManager;
            var res = packageManager.CanRequestPackageInstalls();

            if (!res)
            {
                Platform.CurrentActivity.StartActivity(new Android.Content.Intent(Android.Provider.Settings.ActionManageUnknownAppSources, Android.Net.Uri.Parse("package:" + AppInfo.Current.PackageName)));
                return;
            }

            string path = Path.Combine(Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath, "com.lismondt.asiecshcedule.apk");

            using HttpClient client = new();
            using Stream stream = await client.GetStreamAsync(url);
            using (FileStream fileStream = new(path, FileMode.OpenOrCreate))
            {
                AppSettings.TempFile = fileStream.Name;
                await stream.CopyToAsync(fileStream);
            };

            var context = Android.App.Application.Context;
            Java.IO.File file = new Java.IO.File(path);

            using (Android.Content.Intent install = new Android.Content.Intent(Android.Content.Intent.ActionView))
            {
                Android.Net.Uri apkURI = AndroidX.Core.Content.FileProvider.GetUriForFile(context, context.ApplicationContext.PackageName + ".provider", file);
                install.SetDataAndType(apkURI, "application/vnd.android.package-archive");
                install.AddFlags(Android.Content.ActivityFlags.NewTask);
                install.AddFlags(Android.Content.ActivityFlags.GrantReadUriPermission);
                install.AddFlags(Android.Content.ActivityFlags.ClearTop);
                install.PutExtra(Android.Content.Intent.ExtraNotUnknownSource, true);

                Platform.CurrentActivity.StartActivity(install);
            }


            //if (res)
            //{
            //    var context = Platform.AppContext;
            //    Java.IO.File apkFile = new Java.IO.File(AppSettings.TempFile);
            //    var res1 = apkFile.Exists();
            //    Android.Content.Intent intent = new Android.Content.Intent(Android.Content.Intent.ActionView);

            //    var uri = FileProvider.GetUriForFile(context, context.ApplicationContext.PackageName + ".fileProvider", apkFile);

            //    intent.SetDataAndType(uri, "application/vnd.android.package-archive");
            //    intent.AddFlags(Android.Content.ActivityFlags.NewTask);
            //    intent.AddFlags(Android.Content.ActivityFlags.GrantReadUriPermission);
            //    intent.AddFlags(Android.Content.ActivityFlags.ClearTop);

            //    intent.PutExtra(Android.Content.Intent.ExtraNotUnknownSource, true);
            //    intent.PutExtra("apkPath", AppSettings.TempFile);

            //    Platform.CurrentActivity.StartActivityForResult(intent, 1);
            //}
        }

        public void FreeTempResources()
        {
            string file = AppSettings.TempFile;
            File.Delete(file);
        }
    }
}

#endif