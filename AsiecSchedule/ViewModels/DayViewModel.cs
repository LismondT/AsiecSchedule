using AsiecSchedule.Models;
using System.Collections.ObjectModel;

namespace AsiecSchedule.ViewModels
{
    public class DayViewModel : ObservableCollection<LessonViewModel>
    {
        private DayModel _model;

        public DayViewModel(DayModel model)
        {
            _model = model;
            
            foreach (LessonModel lessonModel in model)
            {
                Add(new LessonViewModel(lessonModel));
            }
        }
        
        public string Name => $"{_model.Date:M}, {_model.Date:dddd}";

        public DateTime Date => _model.Date;
    }
}
