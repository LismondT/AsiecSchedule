using AsiecSchedule.Models;
using AsiecSchedule.Utils;

namespace AsiecSchedule.Views;

public partial class NoteView : ContentPage
{
	private readonly NoteModel _note;
	private Dictionary<ImageSource, string> _imageSourceToPath;
	private Dictionary<string, string> _filenameToPath;
	private readonly List<Stream> _streams;

	public NoteView(NoteModel model)
	{
		InitializeComponent();
		_note = model;
		_imageSourceToPath = GetImageSourceToPath(_note.ImagePaths);
		_filenameToPath = GetFilenameToPath(_note.FilePaths);
		_streams = [];
	}


	protected override void OnAppearing()
	{
		base.OnAppearing();

		LessonInfo.BindingContext = _note.Lesson;
		NoteEditor.Text = _note.Text;

		ImagesCollection.ItemsSource = _imageSourceToPath.Keys;
		FilesCollection.ItemsSource = _filenameToPath.Keys;
	}


    protected override void OnDisappearing()
    {
        base.OnDisappearing();

		foreach (Stream stream in _streams)
		{
			stream.Close();
		}
    }


    private static Dictionary<ImageSource, string> GetImageSourceToPath(List<string> filepaths)
	{
		Dictionary<ImageSource, string> images = [];

		foreach (string filepath in filepaths)
		{
			Stream stream = File.OpenRead(filepath);
			ImageSource source = ImageSource.FromStream(() => stream);

			images.Add(source, filepath);
		}

		return images;
	}


    private static Dictionary<string, string> GetFilenameToPath(List<string> filepaths)
	{
		Dictionary<string, string> names = [];

		foreach (string filepath in filepaths)
		{
			names.Add(Path.GetFileName(filepath), filepath);
		}

		return names;
	}


	private async void ImagesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (sender is CollectionView collection)
			collection.SelectedItem = null;

		if (e.CurrentSelection.Count == 0) return;
		if (e.CurrentSelection[0] is not ImageSource source) return;

		string filepath = _imageSourceToPath[source];

		await Navigation.PushModalAsync(new ImagesViewerView(filepath));
	}


	private async void FilesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (sender is CollectionView collection)
			collection.SelectedItem = null;

        if (e.CurrentSelection.Count == 0) return;
        if (e.CurrentSelection[0] is not string filename) return;

		string filepath = _filenameToPath[filename];

        await Launcher.Default.OpenAsync(new OpenFileRequest("Выберите приложение", new ReadOnlyFile(filepath)));
    }


	private async void BackButton_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}


    private async void RemoveButton_Clicked(object sender, EventArgs e)
	{
		bool deleteNote = await DisplayAlert("Вы точно хотите удалить заметку?", null, "Да", "Нет");

		if (!deleteNote) return;

		NoteUtils.DeleteNote(_note);
		await Navigation.PopModalAsync();
	}
}