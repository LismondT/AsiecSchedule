using AsiecSchedule.Data;
using AsiecSchedule.Data.Asiec;
using AsiecSchedule.Models;
using AsiecSchedule.Utils;
using AsiecSchedule.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AsiecSchedule.Views;

public partial class ScheduleView : ContentPage
{
    private bool _addNoteMode = false;
    private bool _viewUserDays = false;
    public ICommand GoToSettingsCommand => new Command(async () => await Shell.Current.GoToAsync(nameof(SettingsView)));

    public ScheduleView()
    {
        InitializeComponent();
        BindingContext = this;
        AppGlobals.UpdateScheduleDaysCollection = UpdateCollection;
        AppGlobals.ScheduleFillDays = FillDays;
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        NewUserInfoLabel.IsVisible = AppSettings.RequestID == string.Empty;
        ScheduleCollection.ItemsSource = AppGlobals.UserDays ?? AppGlobals.Days;
    }


    private async void GetScheduleButton_Clicked(object sender, EventArgs e)
    {
        DateTime firstDate = FirstDatePicker.Date;
        DateTime lastDate = SecondDatePicker.Date;
        string requestId = AppSettings.RequestID;
        RequestType requestType = AppSettings.RequestType;

        if (requestId == string.Empty)
        {
            await DisplayAlert("Группа не выбрана", "Выберите группу в настройках", "ок");
            return;
        }

        if (firstDate > lastDate)
        {
            await DisplayAlert("Некорректная дата", "Первая дата должна быть меньше второй", "ОК");
            SecondDatePicker.Date = FirstDatePicker.Date;
            return;
        }

        ScheduleModel schedule;
        try
        {
            schedule = await AsiecParser.GetSchedule(requestId, requestType, firstDate, lastDate);
        }
        catch (Exception)
        {
            await DisplayAlert("Ошибка", "В ходе получения расписания произошла ошибка", "ок");
            return;
        }

        if (schedule == null) return;

        ObservableCollection<DayViewModel> dayViewModels = [];
        foreach (DayModel day in schedule?.Days)
        {
            dayViewModels.Add(new DayViewModel(day));
        }

        AppGlobals.UserDays = dayViewModels;
        SetUserDaysMode(true);
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
                                        x.Lesson?.Date == lesson.Date &&
                                        x.Lesson?.StartTime == lesson.StartTime &&
                                        x.Lesson?.PrimaryInformation == lesson.PrimaryInformation))
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
        if (_viewUserDays)
        {
            SetUserDaysMode(false);
        }
        else
        {
            SetAddNoteMode(!_addNoteMode);
        }
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

    private void SetUserDaysMode(bool mode)
    {
        _viewUserDays = mode;

        if (mode)
        {
            ScheduleCollection.ItemsSource = AppGlobals.UserDays;
            AddNoteToolbarItem.IconImageSource = "arrow_left_dark.png";
        }
        else
        {
            AppGlobals.UserDays = null;
            ScheduleCollection.ItemsSource = AppGlobals.Days;
            FirstDatePicker.Date = DateTime.Now;
            SecondDatePicker.Date = DateTime.Now;
            SetAddNoteMode(false);
        }

        UpdateCollection();
    }

    private void UpdateCollection()
    {
        if (AppGlobals.UserDays == null)
        {
            ScheduleCollection.ItemsSource = AppGlobals.Days;
        }
        else
        {
            ScheduleCollection.ItemsSource = AppGlobals.UserDays;
        }
    }

    private async void FillDays()
    {
        try
        {
            ScheduleModel? schedule = await AsiecParser.GetSchedule(AppSettings.RequestID,
                                                                   AppSettings.RequestType,
                                                                   DateTime.Now,
                                                                   DateTime.Now.AddDays(AppGlobals.DaysCount));

            if (schedule != null)
            {
                ObservableCollection<DayViewModel> days = [];
                foreach (DayModel day in schedule.Days)
                {
                    days.Add(new DayViewModel(day));
                }
                AppGlobals.Days = days;
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Не удалось загрузить расписание", null, "ок");
        }
    }
}