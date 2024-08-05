using AsiecSchedule.Models;
using AsiecSchedule.Utils;

namespace AsiecSchedule.Views;

public partial class NoteView : ContentPage
{
    private readonly NoteModel _note;


	public NoteView(NoteModel model)
	{
		InitializeComponent();
		_note = model;
	}


    protected override void OnAppearing()
    {
        base.OnAppearing();

		LessonInfo.BindingContext = _note.Lesson;
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


    private async void ImagesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is CollectionView collection)
            collection.SelectedItem = 0;

        if (e.CurrentSelection[0] is not ImageSource source) return;

        await Navigation.PushModalAsync(new ImagesViewerView(source));
    }


    private async void FilesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{

	}


	private async void RemoveToolbarItem_Clicked(object sender, EventArgs e)
	{
		bool deleteNote = await DisplayAlert("Вы точно хотите удалить заметку?", string.Empty, "Да", "Нет");

		if (!deleteNote) return;

		NoteUtils.DeleteNote(_note);
		await Navigation.PopModalAsync();
	}
}