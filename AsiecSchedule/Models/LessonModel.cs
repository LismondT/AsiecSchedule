using AsiecSchedule.Data;
using AsiecSchedule.Data.Asiec;

namespace AsiecSchedule.Models
{
    public class LessonModel
    {
        private int number;
        private string? name;
        private string? group;
        private string? teacher;
        private string? classroom;
        private string? territory;
        private TimeSpan startTime;
        private TimeSpan endTime;
        private DateTime? date;
        private bool hasNote;

        public int Number { get => number; set => number = value; }
        public string? Name { get => name; set => name = value; }
        public string? Group { get => group; set => group = value; }
        public string? Teacher { get => teacher; set => teacher = value; }
        public string? Classroom { get => classroom; set => classroom = value; }
        public string? Territory { get => territory; set => territory = value; }
        public TimeSpan StartTime { get => startTime; set => startTime = value; }
        public TimeSpan EndTime { get => endTime; set => endTime = value; }
        public DateTime? Date { get => date; set => date = value; }
        public TimeSpan Duration => EndTime - StartTime;
        public bool HasNote { get => hasNote; set => hasNote = value; }
    }
}
