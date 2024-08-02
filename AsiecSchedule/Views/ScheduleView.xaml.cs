using AsiecSchedule.Data;
using AsiecSchedule.Data.Asiec;
using AsiecSchedule.Models;
using AsiecSchedule.Utils;
using AsiecSchedule.ViewModels;

namespace AsiecSchedule.Views;

public partial class ScheduleView : ContentPage
{
    private bool _addNoteMode = false;

    
    public ScheduleView()
    {
        InitializeComponent();

        ScheduleCollection.ItemsSource = AppGlobals.Days;
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
        List<DayModel> days = DebugUtils.GetFilledDays();



        GroupedDaysViewModel daysViewModel = new(days);

        ScheduleCollection.ItemsSource = daysViewModel.Days;
    }

    
    private async void ScheduleCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!_addNoteMode)
        {
            ((CollectionView)sender).SelectedItem = 0;
            return;
        }

        if (e.CurrentSelection[0] is not LessonModel lesson)
            return;

        await Navigation.PushModalAsync(new AddNoteView(lesson));

        ((CollectionView)sender).SelectedItem = 0;
        _addNoteMode = false;
    }


    private void AddNoteToolbarItem_Clicked(object sender, EventArgs e)
    {
        _addNoteMode = true;
    }
}