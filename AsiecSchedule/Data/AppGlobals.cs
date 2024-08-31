using AsiecSchedule.Models;
using AsiecSchedule.ViewModels;
using System.Collections.ObjectModel;

namespace AsiecSchedule.Data
{
    public delegate void UpdateItem();
    public delegate void Callback(bool arg);

    public static class AppGlobals
    {
        public static UpdateItem? FlyoutMenuUpdateRequestIDLabel { get; set; } = null;
        public static Callback? CheckUpdates { get; set; } = null;
        public static UpdateItem? OnPermissionsSuccess { get; set; } = null;
        public static UpdateItem? OnPermissionsDenied { get; set; } = null;
        public static UpdateItem? OnCheckUpdateFailed { get; set; } = null;
        public static UpdateItem? UpdateScheduleDaysCollection { get; set; } = null;
        public static UpdateItem? ScheduleFillDays {  get; set; } = null;

        public const int DaysCount = 14;
        public static ObservableCollection<DayViewModel>? Days { get; set; } = [];
        public static ObservableCollection<DayViewModel>? UserDays { get; set; } = null;
        public static DayViewModel? CurrentDay => Days?[0];

        public static ObservableCollection<NoteModel> Notes { get; set; } = [];
        public static string NotesPath => Path.Combine(FileSystem.CacheDirectory, "notes");
    }
}
