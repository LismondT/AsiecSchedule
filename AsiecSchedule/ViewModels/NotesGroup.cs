using AsiecSchedule.Models;

namespace AsiecSchedule.ViewModels
{
    class NotesGroup(string name, List<NoteModel> notes) : List<NoteModel>(notes)
    {
        public string Name { get; set; } = name;
    }
}
