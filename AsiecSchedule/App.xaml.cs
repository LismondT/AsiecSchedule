using AsiecSchedule.Data;
using AsiecSchedule.Update;
using AsiecSchedule.Utils;

namespace AsiecSchedule
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            
            AppGlobals.Notes = NoteUtils.GetNotes();
        }
    }
}
