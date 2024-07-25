using AsiecSchedule.Data;
using AsiecSchedule.Models;

namespace AsiecSchedule.Views;

public partial class InfoView : ContentPage
{
    private static TimeSpan? _currentStateEndTime;
    private static CurrentState _currentState;

    private enum CurrentState
    {
        Lesson,
        Recess,
        BeforeLessons,
        AfterLessons,
        Weekend,
        Unexpected
    }

    private readonly Dictionary<CurrentState, string> _stateToText = new()
    {
        { CurrentState.Lesson,        string.Empty           },
        { CurrentState.Recess,        "Перемена"             },
        { CurrentState.BeforeLessons, "Пары ещё не начались" },
        { CurrentState.AfterLessons,  "Пары закончились"     },
        { CurrentState.Weekend,       "Выходной :)"          },
        { CurrentState.Unexpected,    "Хз что сейчас"        }
    };

    private readonly Dictionary<CurrentState, string> _stateToTimerText = new()
    {
        { CurrentState.Lesson,        "До конца пары "     },
        { CurrentState.Recess,        "До конца перемены " },
        { CurrentState.BeforeLessons, string.Empty         },
        { CurrentState.AfterLessons,  string.Empty         },
        { CurrentState.Weekend,       string.Empty         },
        { CurrentState.Unexpected,    string.Empty         }
    };

    public InfoView()
    {
        InitializeComponent();

        IDispatcherTimer? timer = Application.Current?.Dispatcher.CreateTimer();

        if (timer != null)
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => UpdateTimers();
            timer.Start();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        (CurrentState, LessonModel?) state = InfoView.GetCurrentState(DateTime.Now.TimeOfDay, GetFilledDays()[0]);
        DisplayCurrentState(state.Item2, state.Item1);
    }

    private List<DayModel> GetFilledDays()
    {
        List<DayModel> days = new List<DayModel>();

        LessonModel firstLesson = new LessonModel()
        {
            Number = 1,
            Name = "Математика (1)",
            Group = "9ИСиП231 (1)",
            Classroom = "200 (1)",
            Territory = "Корпус 2 (1)",
            Teacher = "Завьялова (1)",
            StartTime = new TimeSpan(15, 0, 0),
            EndTime = new TimeSpan(16, 0, 0)
        };

        LessonModel secondLesson = new LessonModel()
        {
            Number = 2,
            Name = "Литература (2)",
            Group = "9ИСиП231 (2)",
            Classroom = "304 (2)",
            Territory = "Корпус 2 (2)",
            Teacher = "Кораблик (2)",
            StartTime = new TimeSpan(16, 30, 0),
            EndTime = new TimeSpan(17, 0, 0)
        };

        LessonModel thirdLesson = new LessonModel()
        {
            Number = 3,
            Name = "Обществознание (3)",
            Group = "9ИСиП231 (3)",
            Classroom = "104 (3)",
            Territory = "Корпус 2 (3)",
            Teacher = "Бакеев Тимур (3)",
            StartTime = new TimeSpan(17, 30, 0),
            EndTime = new TimeSpan(18, 0, 0)
        };

        for (int i = 1; i < 12; i++)
        {
            DateTime date = new(2024, 7, i);
            List<LessonModel> lessons = new List<LessonModel>() { firstLesson, secondLesson, thirdLesson };

            days.Add(new DayModel(date, lessons));
        }

        return days;
    }

    private void UpdateTimers()
    {
        TimeSpan currentTime = DateTime.Now.TimeOfDay;

        string currentTimerText = _stateToTimerText[_currentState];

        if (_currentStateEndTime != null)
        {
            TimeSpan timeToEnd = _currentStateEndTime.Value - currentTime;
            currentTimerText += $"{timeToEnd.Hours} часов {timeToEnd:mm} минут {timeToEnd:ss} секунд";
        }
        else
        {
            currentTimerText = string.Empty;
        }

        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (currentTimerText != string.Empty)
            {
                CurrentStateTimeToEnd.Text = currentTimerText;
            }
        });
    }

    private static (CurrentState, LessonModel?) GetCurrentState(TimeSpan targetTime, DayModel day)
    {
        if (day.Count == 0) return (CurrentState.Weekend, null);

        TimeSpan tmpTime = TimeSpan.Zero;
        bool firstIter = true;

        for (int i = 0; i < day.Count; i++)
        {
            LessonModel lesson = day[i];
            TimeSpan startTime = lesson.StartTime;
            TimeSpan endTime = lesson.EndTime;

            if (targetTime > startTime && targetTime < endTime)
                return (CurrentState.Lesson, day[i]);
            

            if (!firstIter && targetTime >= tmpTime && targetTime <= startTime)
                return (CurrentState.Recess, day[i]);

            tmpTime = endTime;
            firstIter = false;
        }

        if (targetTime < day.First().StartTime)
            return (CurrentState.BeforeLessons, null);

        if (targetTime > day.Last().EndTime)
            return (CurrentState.AfterLessons, null);

        return (CurrentState.Unexpected, null);
    }

    private void DisplayCurrentState(LessonModel? lesson, CurrentState state)
    {
        string title = _stateToText[state];
        CurrentStateTitle.Text = title;
        
        _currentState = state;

        if (state == CurrentState.Lesson)
        {
            CurrentStateText.IsVisible = true;
            CurrentStateInfo.IsVisible = true;
            CurrentStateTimeToEndFrame.IsVisible = true;
            CurrentStateTimeToEnd.IsVisible = true;

            SetCurrentTimer(lesson?.EndTime);

            CurrentStateTitle.Text = lesson?.NameTitle;
            CurrentStateTeacherTitle.Text = lesson?.TeacherTitle;
            CurrentStateLocationTitle.Text = lesson?.Location;

            return;
        }

        if (state == CurrentState.Recess)
        {
            CurrentStateText.IsVisible = false;
            CurrentStateInfo.IsVisible = false;
            CurrentStateTimeToEndFrame.IsVisible = true;
            CurrentStateTimeToEnd.IsVisible = true;

            SetCurrentTimer(lesson?.StartTime);

            return;
        }

        CurrentStateText.IsVisible = false;
        CurrentStateInfo.IsVisible = false;
        CurrentStateTimeToEndFrame.IsVisible = false;
        CurrentStateTimeToEnd.IsVisible = false;
    }

    private void SetCurrentTimer(TimeSpan? target)
    {
        if (target == null) return;

        _currentStateEndTime = target.Value;
    }

    private void DebugSetTimeButton_Clicked(object sender, EventArgs e)
    {
        if (!AppSettings.IsDebug) return;

        TimeSpan targetTime = DebugTimePicker.Time;
        (CurrentState, LessonModel?) state = InfoView.GetCurrentState(targetTime, GetFilledDays()[0]);
        DisplayCurrentState(state.Item2, state.Item1);
    }
}