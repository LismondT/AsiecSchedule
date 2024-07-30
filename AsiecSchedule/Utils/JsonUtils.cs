using AsiecSchedule.Models;
using System.Text.Json;

namespace AsiecSchedule.Utils
{
    public static class JsonUtils
    {
        public static void SaveNote(NoteModel model)
        {
            string json = JsonSerializer.Serialize(model);
            //string path = FileSystem.CacheDirectory + "notes/";
            //string filepath = path + model.ID + ".json";
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes");

            if (!Directory.Exists(filepath)) Directory.CreateDirectory(filepath);

            File.WriteAllText(filepath + $"/{model.ID}.json", json);
        }

        public static List<NoteModel> GetNotes()
        {
            string filespath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes");

            if (!Directory.Exists(filespath)) return [];

            IEnumerable<NoteModel?> noteModels = Directory
                .EnumerateFiles(filespath)
                .Select(filepath => JsonSerializer.Deserialize<NoteModel>(File.ReadAllText(filepath)));

            List<NoteModel> notes = [];

            foreach (NoteModel? noteModel in noteModels)
            {
                if (noteModel == null) continue;
                notes.Add(noteModel);
            }

            return notes;
        }
    }
}
