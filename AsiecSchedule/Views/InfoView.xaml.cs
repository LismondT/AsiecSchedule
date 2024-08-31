using AsiecSchedule.Data;
using AsiecSchedule.Models;
using AsiecSchedule.ViewModels;

namespace AsiecSchedule.Views;

public partial class InfoView : ContentPage
{
    private static TimeSpan? _currentStateEndTime;
    private static State _currentState;

    private enum State
    {
        Lesson,
        Recess,
        BeforeLessons,
        AfterLessons,
        Weekend,
        Unexpected
    }

    private readonly Dictionary<State, string> _currentStateToText = new()
    {
        { State.Lesson,        string.Empty           },
        { State.Recess,        "Перемена"             },
        { State.BeforeLessons, "Пары ещё не начались" },
        { State.AfterLessons,  "Пары закончились"     },
        { State.Weekend,       "Выходной :)"          },
        { State.Unexpected,    "Хз что сейчас"        }
    };

    private readonly Dictionary<State, string> _nextStateToText = new()
    {
        { State.Lesson,        string.Empty       },
        { State.Recess,        "Перемена"         },
        { State.BeforeLessons, string.Empty       },
        { State.AfterLessons,  "Конец пар"        },
        { State.Weekend,       string.Empty       },
        { State.Unexpected,    "Хз что сейчас"    }
    };

    private readonly Dictionary<State, string> _stateToTimerText = new()
    {
        { State.Lesson,        "До конца пары "     },
        { State.Recess,        "До конца перемены " },
        { State.BeforeLessons, string.Empty         },
        { State.AfterLessons,  string.Empty         },
        { State.Weekend,       string.Empty         },
        { State.Unexpected,    string.Empty         }
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



        if (AppSettings.RequestID != string.Empty)
        {
            if (AppGlobals.CurrentDay == null)
            {
                DisplayCurrentState(null, State.Unexpected);
                CurrentStateTitle.Text = "Расписание не было загружено";
                NextStateFrame.IsVisible = false;
            }
            else
            {
                (State, LessonViewModel?) state = AppGlobals.CurrentDay.Date > DateTime.Now
                                                ? (State.Weekend, null)
                                                : GetState(DateTime.Now.TimeOfDay, AppGlobals.CurrentDay);
                DisplayCurrentState(state.Item2, state.Item1);

                if (_currentStateEndTime != null)
                {
                    TimeSpan nextTargetTime = _currentStateEndTime.Value.Add(new TimeSpan(0, 1, 0));
                    (State, LessonViewModel?) nextState = GetState(nextTargetTime, AppGlobals.CurrentDay);
                    DisplayNextState(nextState.Item2, nextState.Item1);
                }
                else
                {
                    NextStateFrame.IsVisible = false;
                }
            }
        }
        else
        {
            DisplayCurrentState(null, State.Unexpected);
            CurrentStateTitle.Text = "Вам следует выбрать группу";
            NextStateFrame.IsVisible = false;
        }

        DebugMenuFrame.IsVisible = AppSettings.IsDebug;
    }

    
    private void UpdateTimers()
    {
        TimeSpan currentTime = DateTime.Now.TimeOfDay;

        string currentTimerText = _stateToTimerText[_currentState];

        if (_currentStateEndTime != null)
        {
            TimeSpan timeToEnd = _currentStateEndTime.Value - currentTime;
            currentTimerText += $"{timeToEnd:hh\\:mm\\:ss}";
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

    
    private static (State, LessonViewModel?) GetState(TimeSpan targetTime, DayViewModel? day)
    {
        if (day == null || day.Count == 0) return (State.Weekend, null);

        TimeSpan tmpTime = TimeSpan.Zero;
        bool firstIter = true;

        for (int i = 0; i < day.Count; i++)
        {
            LessonViewModel lesson = day[i];
            TimeSpan startTime = lesson.StartTime;
            TimeSpan endTime = lesson.EndTime;

            if (targetTime > startTime && targetTime < endTime)
                return (State.Lesson, day[i]);
            

            if (!firstIter && targetTime >= tmpTime && targetTime <= startTime)
                return (State.Recess, day[i]);

            tmpTime = endTime;
            firstIter = false;
        }

        if (targetTime < day.First().StartTime)
            return (State.BeforeLessons, null);

        if (targetTime > day.Last().EndTime)
            return (State.AfterLessons, null);

        return (State.Unexpected, null);
    }

    
    private void DisplayCurrentState(LessonViewModel? lesson, State state)
    {
        string title = _currentStateToText[state];
        CurrentStateTitle.Text = title;
        
        _currentState = state;

        if (state == State.Lesson || state == State.Recess)
        {
            CurrentStateText.IsVisible = true;
        }
        else
        {
            CurrentStateText.IsVisible = false;
        }

        if (state == State.Lesson)
        {
            CurrentStateInfo.IsVisible = true;
            CurrentStateTimeToEndFrame.IsVisible = true;
            CurrentStateTimeToEnd.IsVisible = true;

            SetCurrentTimer(lesson?.EndTime);

            CurrentStateTitle.Text = lesson?.NameTitle;
            CurrentStateTeacherTitle.Text = lesson?.TeacherTitle;
            CurrentStateLocationTitle.Text = lesson?.Location;

            return;
        }

        if (state == State.Recess)
        {
            CurrentStateInfo.IsVisible = false;
            CurrentStateTimeToEndFrame.IsVisible = true;
            CurrentStateTimeToEnd.IsVisible = true;

            SetCurrentTimer(lesson?.StartTime);

            return;
        }

        CurrentStateInfo.IsVisible = false;
        CurrentStateTimeToEndFrame.IsVisible = false;
        CurrentStateTimeToEnd.IsVisible = false;
    }

    
    private void DisplayNextState(LessonViewModel? lesson, State state)
    {
        string title = _nextStateToText[state];
        
        if (title == string.Empty && state != State.Lesson)
        {
            NextStateFrame.IsVisible = false;
            return;
        }
        else
        {
            NextStateFrame.IsVisible = true;
            NextStateInfo.IsVisible = false;
        }

        if (state == State.Recess)
        {
            LessonViewModel? currentLesson = GetState(DateTime.Now.TimeOfDay, AppGlobals.CurrentDay).Item2;
            TimeSpan? recessTime = lesson?.StartTime - currentLesson?.EndTime;

            title += $" ({recessTime:mm} минут)";

            NextStateTitle.Text = title;
            return;
        }

        NextStateTitle.Text = title;

        if (state == State.Lesson)
        {
            NextStateInfo.IsVisible = true;

            NextStateTitle.Text = lesson?.NameTitle;
            NextStateTeacherTitle.Text = lesson?.TeacherTitle;
            NextStateLocationTitle.Text = lesson?.Location;
            return;
        }
    }

    
    private static void SetCurrentTimer(TimeSpan? target)
    {
        if (target == null) return;

        _currentStateEndTime = target.Value;
    }

    
    private void DebugSetTimeButton_Clicked(object sender, EventArgs e)
    {
        if (!AppSettings.IsDebug) return;

        TimeSpan targetTime = DebugTimePicker.Time;
        (State, LessonViewModel?) state = GetState(targetTime, AppGlobals.CurrentDay);
        DisplayCurrentState(state.Item2, state.Item1);

        if (_currentStateEndTime != null)
        {
            TimeSpan nextTargetTime = _currentStateEndTime.Value.Add(new TimeSpan(0, 1, 0));
            (State, LessonViewModel?) nextState = GetState(nextTargetTime, AppGlobals.CurrentDay);
            DisplayNextState(nextState.Item2, nextState.Item1);
        }
        else
        {
            NextStateFrame.IsVisible = false;
        }
    }
}