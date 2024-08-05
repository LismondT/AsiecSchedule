using AsiecSchedule.Data.Asiec;

namespace AsiecSchedule.Data
{
    public static class AppSettings
    {
        public static class Keys
        {
            public const string RequestID = "RequestID";
            public const string RequestType = "RequestType";
        }

        private static string? _requestID;
        private static RequestType _requestType;

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

        public static bool IsDebug { get; set; } = true;

        public static string NotesPath => Path.Combine(FileSystem.CacheDirectory, "notes");
    }
}
