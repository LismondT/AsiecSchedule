using System.Reflection.Metadata.Ecma335;

namespace AsiecSchedule.Models
{
    public class NoteModel
    {
        public int ID { get; set; }
        public LessonModel? Lesson { get; set; }
        public string? Text { get; set; }
        public bool IsConsiderIndependentLoad { get; set; }
        public bool IsForNextLesson { get; set; }
        public List<string> FilePaths { get; set; } = [];
        public List<string> ImagePaths { get; set; } = [];
    }
}
