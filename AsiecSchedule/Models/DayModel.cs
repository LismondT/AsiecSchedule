﻿namespace AsiecSchedule.Models
{
    public class DayModel(DateTime date, List<LessonModel> lessons) : List<LessonModel>(lessons)
    {
        public DateTime Date { get; set; } = date;
    }
}
