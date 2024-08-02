using AsiecSchedule.Models;

namespace AsiecSchedule.Views;

public partial class NoteView : ContentPage
{
	private NoteModel _note;


	public NoteView(NoteModel model)
	{
		InitializeComponent();
		_note = model;
	}


    protected override void OnAppearing()
    {
        base.OnAppearing();

		ImagesCollection.ItemsSource = GetImageSources(_note.ImagePaths);
		FilesCollection.ItemsSource = GetFileNames(_note.FilePaths);
    }


    private List<ImageSource> GetImageSources(List<string> filepaths)
	{
		List<ImageSource> images = [];

		foreach (string filepath in filepaths)
		{
			images.Add(ImageSource.FromFile(filepath));
		}

		return images;
	}


	private List<string> GetFileNames(List<string> filepaths)
	{
		List<string> names = [];

		foreach (string filepath in filepaths)
		{
			names.Add(Path.GetFileName(filepath));
		}

		return names;
	}
}