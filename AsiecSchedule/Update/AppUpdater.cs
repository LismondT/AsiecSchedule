using AsiecSchedule.Data;
using Octokit;

namespace AsiecSchedule.Update
{
    public static class AppUpdater
    {
        private static readonly IUpdater? _updater;
        private static readonly GitHubClient _gitHubClient;
        private static Release? _latestRelease;
        public static string? TempFilePath { get; }
        public static string? AboutUpdate { get; private set; }
        public static bool FailedInit { get; private set; } = true;

        static AppUpdater()
        {
            _gitHubClient = new GitHubClient(new ProductHeaderValue("AsiecSchedule"));
            AboutUpdate = null;


#if ANDROID

            _updater = new AndroidUpdater();
            TempFilePath = Path.Combine(Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath, "com.lismondt.asiecshcedule.apk");

#elif WINDOWS //TODO: Windows update
            
            _updater = new WindowsUpdater();
            TempFilePath = "";
#endif
        }

        public static async Task Init()
        {
            _latestRelease = await GetLatestRelease();
            AboutUpdate = _latestRelease?.Body;

            if (_latestRelease != null)
            {
                FailedInit = false;
            }
            else
            {
                FailedInit = true;
            }
        }

        public static bool CheckPremissions() => _updater?.CheckPermissions() ?? throw new InvalidOperationException();

        public static string GetCurrentVersion()
        {
            string version = AppInfo.Current.VersionString;

#if WINDOWS
            version = string.Join('.', version.Split('.')[.. 3]);
#endif

            return version;
        }

        public static string? GetLatestVersion() => _latestRelease?.TagName;

        public static async Task<Release?> GetLatestRelease()
        {
            Release? release = null;

            try
            {
                release = await _gitHubClient.Repository.Release.GetLatest("LismondT", "AsiecSchedule");
            }
            catch (Exception e)
            {
                AppGlobals.OnCheckUpdateFailed?.Invoke();
            }

            return release;
        }

        public static bool IsLatestVersion()
        {
            string? releaseVersion = GetLatestVersion();
            string appVerion = GetCurrentVersion();

            if (releaseVersion == null) return true;

            return releaseVersion.Replace("v", "") == appVerion;
        }

        public static string? GetLatestVersionDownloadUrl()
        {
            if (_latestRelease == null)
                return null;

            string url = _latestRelease.Assets.Where(x => x.BrowserDownloadUrl.Contains(".apk")).FirstOrDefault()?.BrowserDownloadUrl ?? "";
            return url;
        }

        public static void Update()
        {
            if (_updater == null || TempFilePath == null)
                throw new Exception();

            _updater.Update(TempFilePath);
        }

        public static void FreeTempResources()
        {
            if (_updater == null || TempFilePath == null)
                throw new Exception();

            _updater.FreeTempResources(TempFilePath);
        }
    }
}
