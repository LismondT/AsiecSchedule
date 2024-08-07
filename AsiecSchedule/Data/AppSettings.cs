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
            public const string TempFile = "TempFile";
        }

        private static string? _requestID;
        private static RequestType _requestType;
        private static bool _wasUpdated;
        private static string _tempFile;

        static AppSettings()
        {
            _wasUpdated = Preferences.Get(Keys.WasUpdated, false);
            _tempFile = Preferences.Get(Keys.TempFile, string.Empty);
        }

        public static string RequestID
        {
            get
            {

                if (_requestID == null)
                {
                    string tmp = string.Empty;
                    
                    try
                    {
                        tmp = Preferences.Get(Keys.RequestID, string.Empty);
                    }
                    catch { }

                    _requestID = tmp;
                }

                return _requestID;
            }

            set
            {
                _requestID = value;
                Preferences.Set(Keys.RequestID, value);
            }
        }

        public static RequestType RequestType
        {
            get
            {
                if (_requestType == RequestType.None)
                {
                    RequestType tmp = RequestType.None;

                    try
                    {
                        tmp = (RequestType)Preferences.Get(Keys.RequestType, (int)RequestType.None);
                    }
                    catch { }

                    _requestType = tmp;
                }

                return _requestType;
            }

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

        public static string TempFile
        {
            get => _tempFile;

            set
            {
                Preferences.Set(Keys.TempFile, value);
                _tempFile = value;
            }
        }

        public static bool IsDebug { get; set; } = true;

        public static string NotesPath => Path.Combine(FileSystem.CacheDirectory, "notes");
    }
}
