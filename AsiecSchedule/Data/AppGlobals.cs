using AsiecSchedule.Models;
using AsiecSchedule.Utils;
using System.Collections.ObjectModel;

namespace AsiecSchedule.Data
{
    public delegate void UpdateItem();
    
    public static class AppGlobals
    {
        public static UpdateItem? FlyoutMenuUpdateRequestIDLabel { get; set; } = null;
        public static UpdateItem? CheckUpdates { get; set; } = null;
        public static UpdateItem? OnPermissionsSuccess { get; set; } = null;
        public static UpdateItem? OnPermissionsDenied { get; set; } = null;

        public static ObservableCollection<DayModel> Days { get; set; } = [];
        public static DayModel? CurrentDay => Days[0];

        public static ObservableCollection<NoteModel> Notes { get; set; } = [];
        public static string NotesPath => Path.Combine(FileSystem.CacheDirectory, "notes");
    }
}
