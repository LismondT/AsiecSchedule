namespace AsiecSchedule.Data
{
    public static class AppGlobals
    {
        public delegate void UpdateItem();

        public static UpdateItem? UpdateRequestIDLabel { get; set; }
    }
}
