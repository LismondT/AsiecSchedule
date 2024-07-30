using AsiecSchedule.Models;
using AsiecSchedule.Utils;

namespace AsiecSchedule.Data
{
    public delegate void UpdateItem();
    
    public static class AppGlobals
    {
        public static UpdateItem? FlyoutMenuUpdateRequestIDLabel { get; set; }
        public static List<DayModel> Days { get; set; } = DebugUtils.GetFilledDays();
        public static DayModel CurrentDay { get; set; } = Days[0];

        public static List<NoteModel> Notes { get; set; } = [];
    }
}
