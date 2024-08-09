using Octokit;

namespace AsiecSchedule.Update
{
    public static class AppUpdater
    {
        public static string TempFilePath { get; }
        private static readonly IUpdater? _updater;
        private static readonly GitHubClient _gitHubClient;
        private static Release? _latestRelease;

        static AppUpdater()
        {
            _gitHubClient = new GitHubClient(new ProductHeaderValue("AsiecSchedule"));

#if ANDROID

            _updater = new AndroidUpdater();
            TempFilePath = Path.Combine(Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath, "com.lismondt.asiecshcedule.apk");

#elif WINDOWS //TODO: Windows update
            
            _updater = new WindowsUpdater();

#endif
        }

        public static async Task Init()
        {
            _latestRelease = await GetLatestRelease();
        }

        public static bool CheckPremissions() => _updater?.CheckPermissions() ?? throw new InvalidOperationException();

        public static string GetCurrentVersion() => AppInfo.Current.VersionString;

        public static string GetLatestVersion() => _latestRelease?.TagName ?? throw new Exception();

        public static async Task<Release> GetLatestRelease() => await _gitHubClient.Repository.Release.GetLatest("LismondT", "AsiecSchedule");

        public static bool IsLatestVersion()
        {
            string releaseVersion = GetLatestVersion();
            string appVerion = GetCurrentVersion();

            return releaseVersion.Replace("v", "") == appVerion;
        }

        public static string GetLatestVersionDownloadUrl()
        {
            if (_latestRelease == null)
                throw new Exception();

            string url = _latestRelease.Assets.Where(x => x.BrowserDownloadUrl.Contains(".apk")).FirstOrDefault()?.BrowserDownloadUrl ?? "";
            return url;
        }

        public static void Update()
        {
            if (_updater == null)
                throw new Exception();

            _updater.Update(TempFilePath);
        }

        public static void FreeTempResources()
        {
            if (_updater == null)
                throw new Exception();

            _updater.FreeTempResources(TempFilePath);
        }
    }
}
