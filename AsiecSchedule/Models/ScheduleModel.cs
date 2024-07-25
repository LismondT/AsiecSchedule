namespace AsiecSchedule.Models
{
    public class ScheduleModel
    {
        public DateTime? FirstDate { get; set; }
        public DateTime? LastDate { get; set; }
        public List<DayModel>? Days { get; set; }
    }
}
