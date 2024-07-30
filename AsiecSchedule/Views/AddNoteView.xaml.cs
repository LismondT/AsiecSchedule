using AsiecSchedule.Data;
using AsiecSchedule.Models;
using AsiecSchedule.Utils;
using System.Collections.ObjectModel;

namespace AsiecSchedule.Views;

public partial class AddNoteView : ContentPage
{
    private LessonModel _lesson;

    private List<string> _fileFullpaths;
    private List<string> _imageFullpaths;

    private ObservableCollection<string> _fileNames;
    private ObservableCollection<ImageSource> _imageSources;
	
    public AddNoteView(LessonModel model)
	{
		InitializeComponent();

        _lesson = model;
		LessonStackLayout.BindingContext = model;

        _fileFullpaths = [];
        _imageFullpaths = [];

        _fileNames = [];
        _imageSources = [];

        ImagesCollection.ItemsSource = _imageSources;
        FilesCollection.ItemsSource = _fileNames;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        JsonUtils.SaveNote(new()
        {
            ID = AppGlobals.Notes.Count,
            IsConsiderIndependentLoad = true,
            IsForNextLesson = true,
            Lesson = _lesson,
            FilePaths = _fileFullpaths,
            ImagePaths = _imageFullpaths,
        });
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


    private async void AddFileButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            FileResult? file = await FilePicker.Default.PickAsync();

            if (file != null)
            {
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, file.FileName);

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


    private async void TakePhotoButton_Clicked(object sender, EventArgs e)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

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
            string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

            using Stream sourceStream = await photo.OpenReadAsync();
            using FileStream localFileStream = File.OpenWrite(localFilePath);

            await sourceStream.CopyToAsync(localFileStream);

            _imageFullpaths.Add(localFileStream.Name);
            _imageSources.Add(ImageSource.FromFile(localFilePath));
        }
    }

}