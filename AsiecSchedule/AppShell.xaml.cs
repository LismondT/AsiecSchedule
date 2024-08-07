using AsiecSchedule.Data;

namespace AsiecSchedule
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            AppGlobals.FlyoutMenuUpdateRequestIDLabel = UpdateRequestIDLabel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdateRequestIDLabel();
        }

        private void UpdateRequestIDLabel()
        {
            RequestIDLabel.Text = AppSettings.RequestID == string.Empty
                ? "Выберите группу в настройках"
                : AppSettings.RequestID;
        }
    }
}
