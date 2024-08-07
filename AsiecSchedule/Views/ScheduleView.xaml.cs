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

        await AppUpdater.Init();

        if (AppSettings.WasUpdated)
            AppUpdater.FreeTempResources();

        if (!AppUpdater.IsLatestVersion())
        {
            bool toUpdate = await DisplayAlert("Доступна новая версия",
                $"Текущая версия: {AppUpdater.GetCurrentVersion()}\n" +
                $"Последняя версия: {AppUpdater.GetLatestVersion()}",
                "Обновить", "Не обновлять");

            if (toUpdate)
            {
                await AppUpdater.Update();
            }
        }
        else
        {
            await DisplayAlert("Последняя версия", "Установленна последняя версия", "ок");
        }
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

        Title = mode == false
            ? "Расписание"
            : "Выберите пару";

        if (mode)
        {
            icon = Application.Current?.RequestedTheme == AppTheme.Light
                ? "editor_remove_button_activated.png"
                : "editor_remove_button_activated_dark.png";
        }
        else
        {
            icon = Application.Current?.RequestedTheme == AppTheme.Light
                ? "add_note_toolbar_icon.png"
                : "add_note_toolbar_icon_dark.png";
        }

        AddNoteToolbarItem.IconImageSource = icon;
    }
}