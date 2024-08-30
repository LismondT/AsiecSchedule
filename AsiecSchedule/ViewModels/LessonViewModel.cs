using AsiecSchedule.Data;
using AsiecSchedule.Data.Asiec;
using AsiecSchedule.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AsiecSchedule.ViewModels
{
    public class LessonViewModel : INotifyPropertyChanged
    {
        private readonly LessonModel _model;

        public event PropertyChangedEventHandler? PropertyChanged;

        public LessonViewModel(LessonModel model)
        {
            _model = model;
        }

        public LessonViewModel()
        {
            _model = new();
        }

        public int Number
        {
            get => _model.Number;
            set
            {
                if (_model.Number != value)
                {
                    _model.Number = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Name
        {
            get => _model.Name;
            set
            {
                if (_model.Name != value)
                {
                    _model.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Group
        {
            get => _model.Group;
            set
            {
                if (_model.Group != value)
                {
                    _model.Group = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Teacher
        {
            get => _model.Teacher;
            set
            {
                if (_model.Teacher != value)
                {
                    _model.Teacher = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Classroom
        {
            get => _model.Classroom;
            set
            {
                if (_model.Classroom != value)
                {
                    _model.Classroom = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Territory
        {
            get => _model.Territory;
            set
            {
                if (_model.Territory != value)
                {
                    _model.Territory = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan StartTime
        {
            get => _model.StartTime;
            set
            {
                if (_model.StartTime != value)
                {
                    _model.StartTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan EndTime
        {
            get => _model.EndTime;
            set
            {
                if (_model.EndTime != value)
                {
                    _model.EndTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? Date
        {
            get => _model.Date;
            set
            {
                if (_model.Date != value)
                {
                    _model.Date = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HasNote
        {
            get => _model.HasNote;
            set
            {
                if (_model.HasNote != value)
                {
                    _model.HasNote = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan Duration => EndTime - StartTime;

        public string Preview => $"{Number}. ({StartTime:hh\\:mm}-{EndTime:hh\\:mm})";
        public string? NameTitle => $"Предмет: {Name}";
        public string? GroupTitle => $"Группа: {Group}";
        public string? TeacherTitle => $"Преподаватель: {Teacher}";
        public string? ClassroomTitle => $"Аудитория: {(Classroom != "\"" ? Classroom : "не указано")}";
        public string Location => $"{ClassroomTitle} | {Territory}";
        public string DateTitle
        {
            get
            {
                if (_model.Date != null)
                {
                    return $"{Date:M}, {Date:dddd}";
                }
                return "Дата не обнаруженна";
            }
        }

        public string DurationTitle => $"Длительность: {Duration}";

        public string? PrimaryInformation => AppSettings.RequestType switch
        {
            RequestType.GroupId => TeacherTitle,
            RequestType.TeacherId => GroupTitle,
            RequestType.ClassroomId => TeacherTitle,
            RequestType.None => string.Empty,
            _ => string.Empty
        };

        public string? SecondaryInformation => AppSettings.RequestType switch
        {
            RequestType.GroupId => Location,
            RequestType.TeacherId => Location,
            RequestType.ClassroomId => GroupTitle,
            RequestType.None => string.Empty,
            _ => string.Empty
        };

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
