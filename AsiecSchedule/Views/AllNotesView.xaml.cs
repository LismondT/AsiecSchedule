using AsiecSchedule.Data;
using AsiecSchedule.Models;

namespace AsiecSchedule.Views;

public partial class AllNotesView : ContentPage
{
	public AllNotesView()
	{
		InitializeComponent();

		NotesCollection.ItemsSource = AppGlobals.Notes;
	}

    private async void NotesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0) return;

        if (e.CurrentSelection[0] is not NoteModel note)
            return;

        await Navigation.PushModalAsync(new NoteView(note));

        ((CollectionView)sender).SelectedItem = 0;
    }
}