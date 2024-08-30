using AsiecSchedule.Data.Asiec;

namespace AsiecSchedule.Data
{
    public static class AppSettings
    {
        public static class Keys
        {
            public const string RequestID = "RequestID";
            public const string RequestType = "RequestType";
            public const string WasUpdated = "WasUpdated";
            public const string IsNotifyAboutUpdate = "IsNotifyAboutUpdate";
        }

        private static string _requestID;
        private static RequestType _requestType;
        private static bool _wasUpdated;
        private static bool _isNotifyAboutUpdate;

        static AppSettings()
        {
            try { _requestID = Preferences.Get(Keys.RequestID, string.Empty); }
            catch { _requestID = string.Empty; }

            try { _requestType = (RequestType)Preferences.Get(Keys.RequestType, (int)RequestType.None); }
            catch { _requestType = RequestType.None; }

            try { _wasUpdated = Preferences.Get(Keys.WasUpdated, false); }
            catch { _wasUpdated = false; }
            
            try { _isNotifyAboutUpdate = Preferences.Get(Keys.IsNotifyAboutUpdate, true); }
            catch { _isNotifyAboutUpdate = true; }
        }

        public static string RequestID
        {
            get => _requestID;

            set
            {
                _requestID = value;
                Preferences.Set(Keys.RequestID, value);
            }
        }

        public static RequestType RequestType
        {
            get => _requestType;

            set
            {
                _requestType = value;
                Preferences.Set(Keys.RequestType, (int)_requestType);
            }
        }

        public static bool WasUpdated
        {
            get => _wasUpdated;

            set
            {
                Preferences.Set(Keys.WasUpdated, value);
                _wasUpdated = value;
            }
        }

        public static bool IsNotifyAboutUpdate
        {
            get => _isNotifyAboutUpdate;

            set
            {
                Preferences.Set(Keys.IsNotifyAboutUpdate, value);
                _isNotifyAboutUpdate = value;
            }
        }

        public static bool IsDebug { get; set; } = false;
    }
}
