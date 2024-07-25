using AsiecSchedule.Models;

namespace AsiecSchedule.ViewModels
{
    class GroupedDaysViewModel(List<DayModel> days)
    {
        public List<DayModel> Days { get; private set; } = days;
    }
}
