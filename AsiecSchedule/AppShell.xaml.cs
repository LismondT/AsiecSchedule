using AsiecSchedule.Data;
using AsiecSchedule.Models;
using AsiecSchedule.Popups;
using AsiecSchedule.Update;
using AsiecSchedule.Utils;
using AsiecSchedule.ViewModels;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;

namespace AsiecSchedule
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            AppGlobals.FlyoutMenuUpdateRequestIDLabel = UpdateRequestIDLabel;
            AppGlobals.CheckUpdates = CheckUpdates;
            AppGlobals.OnPermissionsSuccess = OnPermissionsSuccess;
            AppGlobals.OnPermissionsDenied = OnPermissionsDenied;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            UpdateRequestIDLabel();

            ObservableCollection<DayViewModel> daysViewModels = [];
            List<DayModel> days = DebugUtils.GetFilledDays();
            foreach (DayModel dayModel in days)
            {
                daysViewModels.Add(new DayViewModel(dayModel));
            }

            AppGlobals.Days = daysViewModels;
            AppGlobals.Notes = NoteUtils.GetNotes();

            LessonNoteSync();

            await AppUpdater.Init();

            if (AppSettings.IsNotifyAboutUpdate)
                CheckUpdates();
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

        private async void CheckUpdates()
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
    }
}
