using AsiecSchedule.Data;
using AsiecSchedule.Models;
using AsiecSchedule.Utils;
using System.Collections.ObjectModel;
using System.Globalization;

namespace AsiecSchedule.Views;

public partial class AddNoteView : ContentPage
{
    private readonly LessonModel _lesson;
    private readonly string _id;
    private bool _isToRemove = false;

    private readonly List<string> _fileFullpaths;
    private readonly List<string> _imageFullpaths;

    private readonly ObservableCollection<string> _fileNames;
    private readonly ObservableCollection<ImageSource> _imageSources;
	
    public AddNoteView(LessonModel model)
	{
		InitializeComponent();

        _lesson = model;
        _id = Path.GetRandomFileName();
		LessonInfo.BindingContext = _lesson;

        _fileFullpaths = [];
        _imageFullpaths = [];

        _fileNames = [];
        _imageSources = [];

        ImagesCollection.ItemsSource = _imageSources;
        FilesCollection.ItemsSource = _fileNames;
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
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);

                _imageFullpaths.Add(localFileStream.Name);
                _imageSources.Add(ImageSource.FromFile(localFilePath));
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
            using FileStream localFileStream = File.OpenWrite(localFilePath);

            await sourceStream.CopyToAsync(localFileStream);

            _imageFullpaths.Add(localFileStream.Name);
            _imageSources.Add(ImageSource.FromFile(localFilePath));
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
            collection.SelectedItem = 0;

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
            await Launcher.Default.OpenAsync(new OpenFileRequest("????????", new ReadOnlyFile(targetFilepath)));
        }
    }

    private async void ImagesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is CollectionView collection)
            collection.SelectedItem = 0;

        if (e.CurrentSelection[0] is not ImageSource source) return;

        await Navigation.PushModalAsync(new ImagesViewerView(source));
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