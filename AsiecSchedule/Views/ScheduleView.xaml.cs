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
    }


    protected override void OnAppearing()
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
    }


    private async void ScheduleCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ((CollectionView)sender).SelectedItem = null;

        if (!_addNoteMode)
        {
            return;
        }

        if (e.CurrentSelection[0] is not LessonViewModel lesson)
            return;

        SetAddNoteMode(false);
        if (!AppGlobals.Notes.Any(x => x.Lesson?.Name == lesson.Name &&
                                        x.Lesson?.Date == lesson.Date))
        {
            await Navigation.PushModalAsync(new AddNoteView(lesson));
        }
        else
        {
            NoteModel? note = NoteUtils.FindNote(lesson);

            if (note == null) return;

            await Navigation.PushModalAsync(new AddNoteView(note));
        }
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
            ? "add_note_toolbar_activated_icon.png"
            : "add_note_toolbar_icon.png";

        AddNoteToolbarItem.IconImageSource = icon;
    }
}