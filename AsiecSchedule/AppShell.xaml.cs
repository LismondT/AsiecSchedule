using AsiecSchedule.Data;

namespace AsiecSchedule
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            AppGlobals.UpdateRequestIDLabel = UpdateRequestIDLabel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdateRequestIDLabel();
        }

        private void UpdateRequestIDLabel()
        {
            RequestIDLabel.Text = AppSettings.RequestID;
        }
    }
}
