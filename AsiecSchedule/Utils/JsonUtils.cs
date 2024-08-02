using AsiecSchedule.Data;
using AsiecSchedule.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace AsiecSchedule.Utils
{
    public static class JsonUtils
    {
        public static void SaveNote(NoteModel model)
        {
            string json = JsonSerializer.Serialize(model);
            string filepath = AppSettings.NotesPath;

            if (!Directory.Exists(filepath)) Directory.CreateDirectory(filepath);

            File.WriteAllText(filepath + $"/{model.ID}.json", json);
        }

        public static ObservableCollection<NoteModel> GetNotes()
        {
            string filespath = AppSettings.NotesPath;

            if (!Directory.Exists(filespath)) return [];

            IEnumerable<NoteModel?> noteModels = Directory
                .EnumerateFiles(filespath)
                .Select(filepath => JsonSerializer.Deserialize<NoteModel>(File.ReadAllText(filepath)));

            ObservableCollection<NoteModel> notes = [];

            foreach (NoteModel? noteModel in noteModels)
            {
                if (noteModel == null) continue;
                notes.Add(noteModel);
            }

            return notes;
        }
    }
}
