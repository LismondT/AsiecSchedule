using AsiecSchedule.Data;
using AsiecSchedule.Models;
using AsiecSchedule.Utils;
using AsiecSchedule.ViewModels;
using System.Collections.ObjectModel;

namespace AsiecSchedule.Views;

public partial class AddNoteView : ContentPage
{
    private bool _isEditMode = false;
    private readonly NoteModel? _editedNote;

    private bool _isToRemove = false;
    private readonly LessonViewModel _lesson;
    private readonly string _id;

    private readonly List<Stream> _streams;

    private readonly List<string> _fileFullpaths;
    private readonly List<string> _imageFullpaths;

    private readonly ObservableCollection<string> _fileNames;
    private readonly ObservableCollection<ImageSource> _imageSources;
    private readonly Dictionary<ImageSource, string> _imageSourceToPath;


    public AddNoteView(LessonViewModel model, bool isLessonBusy = false)
    {
        InitializeComponent();

        _lesson = model;

        if (isLessonBusy)
        {
            LessonViewModel? nextLesson = FindNextLesson();

            if (nextLesson != null)
            {
                _lesson = nextLesson;
            }
            else
            {
                _lesson = new()
                {
                    Date = null,
                    Name = model.Name,
                    Group = model.Group,
                    Teacher = model.Teacher,
                    Territory = model.Territory,
                    Classroom = model.Classroom,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    HasNote = true
                };
            }
        }

        _id = Path.GetRandomFileName();
        LessonInfo.BindingContext = _lesson;

        _streams = [];

        _fileFullpaths = [];
        _imageFullpaths = [];

        _fileNames = [];
        _imageSources = [];
        _imageSourceToPath = [];

        ImagesCollection.ItemsSource = _imageSources;
        FilesCollection.ItemsSource = _fileNames;
    }


    //Edit note
    public AddNoteView(NoteModel note)
    {
        InitializeComponent();

        _isEditMode = true;
        _editedNote = note;

        _id = _editedNote.ID ?? throw new Exception();
        _lesson = _editedNote.Lesson ?? throw new Exception();

        _streams = [];

        _fileFullpaths = [];
        _imageFullpaths = [];

        _fileNames = [];
        _imageSources = [];
        _imageSourceToPath = [];

        foreach (var filepath in note.ImagePaths)
        {
            AddImage(filepath);
        }

        foreach (var filepath in note.FilePaths)
        {
            AddFile(filepath);
        }

        NoteEditor.Text = note.Text;
        LessonInfo.BindingContext = _lesson;
        ImagesCollection.ItemsSource = _imageSources;
        FilesCollection.ItemsSource = _fileNames;
    }


    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        foreach (Stream stream in _streams)
        {
            stream.Close();
        }
    }


    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        foreach (string path in _fileFullpaths)
        {
            if (_isEditMode &&
                _editedNote != null &&
                _editedNote.FilePaths.Contains(path))
                continue;

            File.Delete(path);
        }

        foreach (string path in _imageFullpaths)
        {
            if (_isEditMode &&
                _editedNote != null &&
                _editedNote.ImagePaths.Contains(path))
                continue;

            File.Delete(path);
        }

        if (!_isEditMode && _editedNote == null)
        {
            Directory.Delete(NoteUtils.GetNoteResourcesFolderPath(_id));
            Directory.Delete(NoteUtils.GetNoteFolderPath(_id));
        }

        await Navigation.PopModalAsync();
    }


    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (IsForNextLessonCheckBox.IsChecked == false)
        {
            _lesson.HasNote = true;
        }

        NoteModel model = new()
        {
            ID = _id,
            Text = NoteEditor.Text,
            IsConsiderIndependentLoad = IsConsiderIndependentLoadCheckBox.IsChecked,
            IsForNextLesson = IsForNextLessonCheckBox.IsChecked,
            Lesson = _lesson,
            FilePaths = _fileFullpaths,
            ImagePaths = _imageFullpaths,
        };

        if (_isEditMode)
        {
            File.Delete(NoteUtils.GetNotePath(model));
            AppGlobals.Notes.Remove(_editedNote);
        }

        AppGlobals.Notes.Add(model);
        NoteUtils.SaveNote(model);

        await Navigation.PopModalAsync();
    }


    private async void TakePhotoButton_Clicked(object sender, EventArgs e)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                string localFilePath = Path.Combine(NoteUtils.GetNoteResourcesFolderPath(_id), photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using (FileStream localFileStream = File.OpenWrite(localFilePath))
                {
                    await sourceStream.CopyToAsync(localFileStream);
                };

                Stream stream = File.OpenRead(localFilePath);
                ImageSource source = ImageSource.FromStream(() => stream);

                _imageFullpaths.Add(localFilePath);
                _imageSources.Add(source);
                _imageSourceToPath.Add(source, localFilePath);
                _streams.Add(stream);
            }
        }
    }


    private async void AddImageButton_Clicked(object sender, EventArgs e)
    {
        FileResult? photo = await MediaPicker.PickPhotoAsync();

        if (photo != null)
        {
            string localFilePath = Path.Combine(NoteUtils.GetNoteResourcesFolderPath(_id), photo.FileName);

            using Stream sourceStream = await photo.OpenReadAsync();
            using (FileStream localFileStream = File.OpenWrite(localFilePath))
            {
                await sourceStream.CopyToAsync(localFileStream);
            }

            AddImage(localFilePath);
        }
    }


    private async void AddFileButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            FileResult? file = await FilePicker.Default.PickAsync();

            if (file != null)
            {
                string localFilePath = Path.Combine(NoteUtils.GetNoteResourcesFolderPath(_id), file.FileName);

                using Stream sourceStream = await file.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);

                AddFile(localFilePath);
            }
        }
        catch
        {
            await DisplayAlert("Ошибка", "Произошла непредвиденная ошибка.", "ок");
        }
    }


    private void RemoveButton_Clicked(object sender, EventArgs e)
    {
        SetRemoveState(!_isToRemove);
    }


    private async void FilesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string targetFilepath = "";

        if (sender is CollectionView collection)
            collection.SelectedItem = null;

        if (e.CurrentSelection.Count == 0) return;

        if (e.CurrentSelection[0] is not string targetFilename) return;

        foreach (string path in _fileFullpaths)
        {
            string filename = Path.GetFileName(path);

            if (filename == targetFilename)
            {
                targetFilepath = path;
                break;
            }
        }

        if (_isToRemove)
        {
            File.Delete(targetFilepath);
            _fileFullpaths.Remove(targetFilepath);
            _fileNames.Remove(targetFilename);
            SetRemoveState(false);
        }
        else
        {
            await Launcher.Default.OpenAsync(new OpenFileRequest("Выберите приложение", new ReadOnlyFile(targetFilepath)));
        }
    }


    private async void ImagesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is CollectionView collection)
            collection.SelectedItem = null;

        if (e.CurrentSelection.Count == 0) return;
        if (e.CurrentSelection[0] is not ImageSource source) return;

        string filepath = _imageSourceToPath[source];

        if (_isToRemove)
        {

            File.Delete(filepath);
            _imageFullpaths.Remove(filepath);
            _imageSourceToPath.Remove(source);
            _imageSources.Remove(source);
            SetRemoveState(false);
        }
        else
        {
            await Navigation.PushModalAsync(new ImagesViewerView(filepath));
        }
    }


    private void IsForNextLessonCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        IsConsiderIndependentLoadLabel.IsVisible = e.Value;
        IsConsiderIndependentLoadCheckBox.IsVisible = e.Value;
    }


    private void SetRemoveState(bool isRemoveMode)
    {
        _isToRemove = isRemoveMode;

        string icon;

        if (!_isToRemove)
        {
            icon = Application.Current?.RequestedTheme == AppTheme.Light
                ? "editor_remove_button.png"
                : "editor_remove_button_dark.png";
        }
        else
        {
            icon = Application.Current?.RequestedTheme == AppTheme.Light
                ? "editor_remove_button_activated.png"
                : "editor_remove_button_activated_dark.png";
        }

        RemoveButton.Source = icon;
    }


    private LessonViewModel? FindNextLesson()
    {
        return AppGlobals.Days
        .Where(day => day.Date > _lesson.Date) // Filter days that are after the current lesson's date
        .SelectMany(day => day) // Flatten the collection to work with individual lessons
        .Where(lesson => lesson.Name == _lesson.Name &&
                         lesson.PrimaryInformation == _lesson.PrimaryInformation) // Filter lessons by name and primary information
        .OrderBy(lesson => lesson.Date) // Order the lessons by date
        .FirstOrDefault(); // Get the first matching lesson
    }


    private void AddImage(string filepath)
    {
        Stream stream = File.OpenRead(filepath);
        ImageSource source = ImageSource.FromStream(() => stream);

        _imageFullpaths.Add(filepath);
        _imageSources.Add(source);
        _imageSourceToPath.Add(source, filepath);
        _streams.Add(stream);
    }


    private void AddFile(string filepath)
    {
        _fileFullpaths.Add(filepath);
        _fileNames.Add(Path.GetFileName(filepath));
    }
}