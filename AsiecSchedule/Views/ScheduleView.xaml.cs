using AsiecSchedule.Data.Asiec;
using AsiecSchedule.Models;
using AsiecSchedule.ViewModels;

namespace AsiecSchedule.Views;

public partial class ScheduleView : ContentPage
{
    public ScheduleView()
    {
        InitializeComponent();
    }

    private async void GetScheduleButton_Clicked(object sender, EventArgs e)
    {
        DateTime firstDate = FirstDatePicker.Date.AddDays(-60);
        DateTime lastDate = SecondDatePicker.Date;
        string requestId = "9ИСиП231"; //AppSettings.RequestId;
        RequestType requestType = RequestType.GroupId; //AppSettings.RequestType;

        if (requestId == string.Empty || requestId == null) return;

        if (firstDate > lastDate)
        {
            await DisplayAlert("Некорректная дата", "Первая дата должна быть меньше второй", "ОК");
            return;
        }

        //Schedule schedule = await AsiecParser.GetSchedule(requestId, requestType, firstDate, lastDate);

        //LoadSchedule(schedule);
        
        //Debug
        List<DayModel> days = GetFilledDays();



        GroupedDaysViewModel daysViewModel = new GroupedDaysViewModel(days);

        ScheduleCollection.ItemsSource = daysViewModel.Days;
    }

    private void LoadSchedule(ScheduleModel schedule)
    {
        if (schedule == null || schedule.Days == null)
            return;

        ScheduleCollection.ItemsSource = schedule.Days;
    }


    private List<DayModel> GetFilledDays()
    {
        List<DayModel> days = new List<DayModel>();

        LessonModel firstLesson = new LessonModel()
        {
            Name = "FirstLessonNamePlaceholder",
            Group = "FirstLessonGroupPlaceholder",
            Classroom = "FirstLessonClassroomPlaceholder",
            Territory = "FirstLessonTerritoryPlaceholder",
            Teacher = "FirstLessonTeacherPlaceholder",
            StartTime = new TimeSpan(16, 0, 0),
            EndTime = new TimeSpan(16, 30, 0)
        };

        LessonModel secondLesson = new LessonModel()
        {
            Name = "SecondLessonNamePlaceholder",
            Group = "SecondLessonGroupPlaceholder",
            Classroom = "SecondLessonClassroomPlaceholder",
            Territory = "SecondLessonTerritoryPlaceholder",
            Teacher = "SecondLessonTeacherPlaceholder",
            StartTime = new TimeSpan(16, 30, 0),
            EndTime = new TimeSpan(17, 0, 0)
        };

        LessonModel thirdLesson = new LessonModel()
        {
            Name = "ThirdLessonNamePlaceholder",
            Group = "ThirdLessonGroupPlaceholder",
            Classroom = "ThirdLessonClassroomPlaceholder",
            Territory = "ThirdLessonTerritoryPlaceholder",
            Teacher = "ThirdLessonTeacherPlaceholder",
            StartTime = new TimeSpan(17, 30, 0),
            EndTime = new TimeSpan(18, 0, 0)
        };

        for (int i = 1; i < 12; i++)
        {
            DateTime date = new(2024, 7, i);
            List<LessonModel> lessons = new List<LessonModel>() { firstLesson, secondLesson, thirdLesson };

            days.Add(new DayModel(date, lessons));
        }

        return days;
    }
}