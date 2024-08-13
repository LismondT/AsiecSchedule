using AsiecSchedule.Data.Asiec;
using AsiecSchedule.Models;
using System.Collections.ObjectModel;

namespace AsiecSchedule.Utils
{
    public static class DebugUtils
    {
        public static ObservableCollection<DayModel> GetFilledDays()
        {
            ObservableCollection<DayModel> days = [];

            string[] names = ["Математика", "Обществознание", "Литература", "Биология", "География"];
            string[] groups = [.. AsiecData.GroupIDs.Keys];
            string[] classrooms = [.. AsiecData.ClassroomIDs.Keys];
            string[] territory = ["Корпус 1", "Коорпус 2"];
            string[] teachers = [.. AsiecData.TeacherIDs.Keys];

            for (int i = 1; i < 12; i++)
            {
                DateTime date = new(2024, 7, i);
                List<LessonModel> lessons = [];
                int lessonsCount = Random.Shared.Next(2, 5);

                for (int j = 1; j < lessonsCount; j++)
                {
                    lessons.Add(new LessonModel()
                    {
                        Number = j,
                        Name = names[Random.Shared.Next(names.Length)],
                        Group = groups[Random.Shared.Next(groups.Length)],
                        Classroom = classrooms[Random.Shared.Next(classrooms.Length)],
                        Territory = territory[Random.Shared.Next(territory.Length)],
                        Teacher = teachers[Random.Shared.Next(teachers.Length)],
                        StartTime = new TimeSpan(j, 30, 0),
                        EndTime = new TimeSpan(j+1, 00, 0),
                        HasNote = false,
                        Date = date
                    });
                }

                days.Add(new DayModel(date, lessons));
            }

            return days;
        }
    }
}
