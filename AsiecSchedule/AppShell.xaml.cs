using AsiecSchedule.Data;
using AsiecSchedule.Data.Asiec;
using AsiecSchedule.Models;
using AsiecSchedule.Popups;
using AsiecSchedule.Update;
using AsiecSchedule.Utils;
using AsiecSchedule.ViewModels;
using AsiecSchedule.Views;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;

namespace AsiecSchedule
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SettingsView), typeof(SettingsView));

            AppGlobals.FlyoutMenuUpdateRequestIDLabel = UpdateRequestIDLabel;
            AppGlobals.CheckUpdates = CheckUpdates;
            AppGlobals.OnCheckUpdateFailed = OnCheckUpdateFailed;
            AppGlobals.OnPermissionsSuccess = OnPermissionsSuccess;
            AppGlobals.OnPermissionsDenied = OnPermissionsDenied;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            UpdateRequestIDLabel();

            if (AppSettings.RequestID != string.Empty)
            {
                ScheduleModel? schedule = null;

                try
                {
                    schedule = await AsiecParser.GetSchedule(AppSettings.RequestID,
                                                         AppSettings.RequestType,
                                                         DateTime.Now,
                                                         DateTime.Now.AddDays(14));
                }
                catch (Exception) {
                    await DisplayAlert("Ошибка", "Не удалось загрузить расписание", "ок");
                    AppGlobals.Days = null;
                    AppGlobals.UpdateScheduleDaysCollection?.Invoke();
                }

                if (schedule != null)
                {
                    ObservableCollection<DayViewModel> daysViewModels = [];
                    foreach (DayModel dayModel in schedule.Days)
                    {
                        daysViewModels.Add(new DayViewModel(dayModel));
                    }
                    AppGlobals.Days = daysViewModels;
                    AppGlobals.UpdateScheduleDaysCollection?.Invoke();
                }
            }
            
            AppGlobals.Notes = NoteUtils.GetNotes();

            LessonNoteSync();

            await AppUpdater.Init();

            if (AppSettings.IsNotifyAboutUpdate)
                CheckUpdates(false);
        }

        private static void LessonNoteSync()
        {
            foreach (NoteModel note in AppGlobals.Notes)
            {
                foreach (DayViewModel day in AppGlobals.Days)
                {
                    foreach (LessonViewModel lesson in day)
                    {
                        if (lesson.Name == note.Lesson?.Name &&
                            lesson.Date == note.Lesson?.Date &&
                            lesson.Number == note.Lesson?.Number &&
                            lesson.PrimaryInformation == note.Lesson?.PrimaryInformation)
                        {
                            lesson.HasNote = true;
                        }
                    }
                }
            }
        }

        private void UpdateRequestIDLabel()
        {
            RequestIDLabel.Text = AppSettings.RequestID == string.Empty
                ? "Выберите группу в настройках"
                : AppSettings.RequestID;
        }

        private async void CheckUpdates(bool notifyAboutLatestUpdate)
        {
            if (!AppUpdater.IsLatestVersion())
            {
                bool toUpdate = await DisplayAlert("Доступна новая версия",
                    $"Текущая версия: v{AppUpdater.GetCurrentVersion()}\n" +
                    $"Последняя версия: {AppUpdater.GetLatestVersion()}\n\n" +
                    $"О новой версии:\n" +
                    AppUpdater.AboutUpdate +
                    "\n\nВы можете обновить приложение в настройках позже",
                    "обновить", "обновить позже");

                if (toUpdate)
                {
                    if (AppUpdater.CheckPremissions())
                    {
                        OnPermissionsSuccess();
                    }
                }
            }
            else
            {
                if (notifyAboutLatestUpdate)
                {
                    await DisplayAlert("Установлена последняя версия", null, "ок");
                }
            }
        }

        private async void OnPermissionsSuccess()
        {
            Page page = Application.Current?.MainPage ?? throw new NullReferenceException();
            await page.ShowPopupAsync(new UpdatePopup());
        }

        private async void OnPermissionsDenied()
        {
            await DisplayAlert("ಠಿ_ಠ", "Без данного разрешения установить новую версию не выйдет\nВы можете обновить приложение в настройках позже", "ок");
        }

        private async void OnCheckUpdateFailed()
        {
            await DisplayAlert("Не удалось получить информацию об обновлении", "Возможно отсутствует интернет соединение", "ок");
        }
    }
}
