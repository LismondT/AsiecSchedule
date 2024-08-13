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
        private DateTime date;
        private bool hasNote;

        public int Number { get => number; set => number = value; }
        public string? Name { get => name; set => name = value; }
        public string? Group { get => group; set => group = value; }
        public string? Teacher { get => teacher; set => teacher = value; }
        public string? Classroom { get => classroom; set => classroom = value; }
        public string? Territory { get => territory; set => territory = value; }
        public TimeSpan StartTime { get => startTime; set => startTime = value; }
        public TimeSpan EndTime { get => endTime; set => endTime = value; }
        public DateTime Date { get => date; set => date = value; }
        public TimeSpan Duration => EndTime - StartTime;
        public bool HasNote { get => hasNote; set => hasNote = value; }

        //public string Preview => $"{Number}. ({StartTime:hh\\:mm}-{EndTime:hh\\:mm})";
        //public string? NameTitle => $"Предмет: {Name}";
        //public string? GroupTitle => $"Группа: {Group}";
        //public string? TeacherTitle => $"Преподаватель: {Teacher}";
        //public string? ClassroomTitle => $"Аудитория: {Classroom}";
        //public string Location => $"{ClassroomTitle} | {Territory}";
        //public string DateTitle => $"{Date:M}, {Date:dddd}";
        //public string DurationTitle => $"Длительность: {Duration}";

        //public string? PrimaryInformation => AppSettings.RequestType switch
        //{
        //    RequestType.GroupId => TeacherTitle,
        //    RequestType.TeacherId => GroupTitle,
        //    RequestType.ClassroomId => TeacherTitle,
        //    RequestType.None => string.Empty,
        //    _ => string.Empty
        //};

        //public string? SecondaryInformation => AppSettings.RequestType switch
        //{
        //    RequestType.GroupId => Location,
        //    RequestType.TeacherId => Location,
        //    RequestType.ClassroomId => GroupTitle,
        //    RequestType.None => string.Empty,
        //    _ => string.Empty
        //};
    }
}
