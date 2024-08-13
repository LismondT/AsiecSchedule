using AsiecSchedule.ViewModels;

namespace AsiecSchedule.Models
{
    public class NoteModel
    {
        public string? ID { get; set; }
        public LessonViewModel? Lesson { get; set; }
        public string? Text { get; set; }
        public bool IsConsiderIndependentLoad { get; set; }
        public bool IsForNextLesson { get; set; }
        public List<string> FilePaths { get; set; } = [];
        public List<string> ImagePaths { get; set; } = [];
    }
}
