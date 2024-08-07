using AsiecSchedule.Data;
using AsiecSchedule.Models;
using AsiecSchedule.Utils;
using System.Collections.ObjectModel;

namespace AsiecSchedule.Views;

public partial class AddNoteView : ContentPage
{
    private bool _isToRemove = false;
    private readonly LessonModel _lesson;
    private readonly string _id;

    private readonly List<Stream> _streams;

    private readonly List<string> _fileFullpaths;
    private readonly List<string> _imageFullpaths;

    private readonly ObservableCollection<string> _fileNames;
    private readonly ObservableCollection<ImageSource> _imageSources;
    private readonly Dictionary<ImageSource, string> _imageSourceToPath;

    
    public AddNoteView(LessonModel model)
    {
        InitializeComponent();

        _lesson = model;
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
            File.Delete(path);
        }

        foreach (string path in _imageFullpaths)
        {
            File.Delete(path);
        }

        Directory.Delete(NoteUtils.GetNoteResourcesFolderPath(_id));
        Directory.Delete(NoteUtils.GetNoteFolderPath(_id));

        await Navigation.PopModalAsync();
    }
    
    
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
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

            Stream stream = File.OpenRead(localFilePath);
            ImageSource source = ImageSource.FromStream(() => stream);

            _imageFullpaths.Add(localFilePath);
            _imageSources.Add(source);
            _imageSourceToPath.Add(source, localFilePath);
            _streams.Add(stream);
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

                _fileFullpaths.Add(localFileStream.Name);
                _fileNames.Add(Path.GetFileName(localFileStream.Name));
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
            await Launcher.Default.OpenAsync(new OpenFileRequest("Выберете приложение", new ReadOnlyFile(targetFilepath)));
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
}