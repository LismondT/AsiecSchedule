using AsiecSchedule.Models;
using AsiecSchedule.Utils;
using System.Collections.ObjectModel;

namespace AsiecSchedule.Data
{
    public delegate void UpdateItem();
    
    public static class AppGlobals
    {
        public static UpdateItem? FlyoutMenuUpdateRequestIDLabel { get; set; }
        public static UpdateItem? CheckUpdates {  get; set; }
        public static UpdateItem? OnPermissionsSuccess { get; set; }
        public static UpdateItem? OnPermissionsDenied { get; set; }

        public static List<DayModel> Days { get; set; } = DebugUtils.GetFilledDays();
        public static DayModel CurrentDay { get; set; } = Days[0];

        public static ObservableCollection<NoteModel> Notes { get; set; } = [];
    }
}
