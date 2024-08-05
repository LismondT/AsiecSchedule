using AsiecSchedule.Data;
using AsiecSchedule.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace AsiecSchedule.Utils
{
    public static class NoteUtils
    {
        public static void SaveNote(NoteModel model)
        {
            string json = JsonSerializer.Serialize(model);
            string filepath = GetNotePath(model);

            if (!Directory.Exists(filepath))
                Directory.CreateDirectory(filepath);

            File.WriteAllText(filepath, json);
        }

        public static ObservableCollection<NoteModel> GetNotes()
        {
            string foldersPath = AppSettings.NotesPath;

            if (!Directory.Exists(foldersPath)) return [];

            IEnumerable<NoteModel?> noteModels = Directory
                .EnumerateDirectories(foldersPath)
                .Select(folderPath => JsonSerializer.Deserialize<NoteModel>(File.ReadAllText($"{folderPath}/note.json")));

            ObservableCollection<NoteModel> notes = [];

            foreach (NoteModel? noteModel in noteModels)
            {
                if (noteModel == null) continue;
                notes.Add(noteModel);
            }

            return notes;
        }

        public static void DeleteNote(NoteModel model) => DeleteNote(model.ID);

        public static void DeleteNote(string? id)
        {
            if (id == null) return;

            NoteModel? note = AppGlobals.Notes.FirstOrDefault(x => x.ID == id);

            if (note == null) return;
            string notePath = GetNotePath(note);

            foreach (string filepath in note.FilePaths)
            {
                File.Delete(filepath);
            }

            foreach (string filepath in note.ImagePaths)
            {
                File.Delete(filepath);
            }

            AppGlobals.Notes.Remove(note);
            File.Delete(notePath);
        }


        private static string GetNoteFolderPath(NoteModel note)
        {
            string folderPath = AppSettings.NotesPath + $"/{note.ID}/";

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return folderPath;
        }


        private static string GetNotePath(NoteModel note) => GetNoteFolderPath(note) + "note.json";


        public static string GetNoteResourcesFolderPath(NoteModel note)
        {
            string folderPath = GetNoteFolderPath(note) + "res/";

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return folderPath;
        }

        public static string GetNoteResourcesFolderPath(string id)
        {
            return GetNoteResourcesFolderPath(new NoteModel()
            {
                ID = id
            });
        }
    }
}
