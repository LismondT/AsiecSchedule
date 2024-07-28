using AsiecSchedule.Data;
using AsiecSchedule.Data.Asiec;
using AsiecSchedule.Models;
using AsiecSchedule.Utils;
using AsiecSchedule.ViewModels;

namespace AsiecSchedule.Views;

public partial class ScheduleView : ContentPage
{
    public ScheduleView()
    {
        InitializeComponent();

        ScheduleCollection.ItemsSource = AppGlobals.Days;
    }

    private async void GetScheduleButton_Clicked(object sender, EventArgs e)
    {
        DateTime firstDate = FirstDatePicker.Date.AddDays(-60);
        DateTime lastDate = SecondDatePicker.Date;
        string requestId = "9����231"; //AppSettings.RequestId;
        RequestType requestType = RequestType.GroupId; //AppSettings.RequestType;

        if (requestId == string.Empty || requestId == null) return;

        if (firstDate > lastDate)
        {
            await DisplayAlert("������������ ����", "������ ���� ������ ���� ������ ������", "��");
            return;
        }

        //Schedule schedule = await AsiecParser.GetSchedule(requestId, requestType, firstDate, lastDate);

        //LoadSchedule(schedule);
        
        //Debug
        List<DayModel> days = DebugUtils.GetFilledDays();



        GroupedDaysViewModel daysViewModel = new(days);

        ScheduleCollection.ItemsSource = daysViewModel.Days;
    }

    private void LoadSchedule(ScheduleModel schedule)
    {
        if (schedule == null || schedule.Days == null)
            return;

        ScheduleCollection.ItemsSource = schedule.Days;
    }

    private void ScheduleCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not LessonModel lesson) return;

        DisplayAlert(lesson.Name, lesson.TeacherTitle, "OK");

        ((CollectionView)sender).SelectedItem = null;
    }

    private void AddNoteToolbarItem_Clicked(object sender, EventArgs e)
    {
       
    }
}