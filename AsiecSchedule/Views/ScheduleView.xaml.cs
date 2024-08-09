using AsiecSchedule.Data;
using AsiecSchedule.Data.Asiec;
using AsiecSchedule.Models;
using AsiecSchedule.Update;
using AsiecSchedule.Utils;
using AsiecSchedule.ViewModels;

namespace AsiecSchedule.Views;

public partial class ScheduleView : ContentPage
{
    private bool _addNoteMode = false;

    
    public ScheduleView()
    {
        InitializeComponent();
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

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
        ((CollectionView)sender).SelectedItem = null;

        if (!_addNoteMode)
        {
            return;
        }

        if (e.CurrentSelection[0] is not LessonModel lesson)
            return;

        SetAddNoteMode(false);
        await Navigation.PushModalAsync(new AddNoteView(lesson));
    }


    private void AddNoteToolbarItem_Clicked(object sender, EventArgs e)
    {
        SetAddNoteMode(!_addNoteMode);
    }


    private void SetAddNoteMode(bool mode)
    {
        string icon;

        _addNoteMode = mode;

        Title = mode
            ? "Выберите пару"
            : "Расписание";

        icon = mode
            ? "editor_remove_button_activated_dark.png"
            : "add_note_toolbar_icon_dark.png";

        AddNoteToolbarItem.IconImageSource = icon;
    }
}