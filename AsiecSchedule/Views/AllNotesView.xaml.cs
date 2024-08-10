using AsiecSchedule.Data;
using AsiecSchedule.Models;
using AsiecSchedule.ViewModels;

namespace AsiecSchedule.Views;

public partial class AllNotesView : ContentPage
{
	public AllNotesView()
	{
		InitializeComponent();
	}


    protected override void OnAppearing()
    {
        base.OnAppearing();

        NotesCollection.ItemsSource = GetNotesGroups();
    }


    private async void NotesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0) return;

        if (e.CurrentSelection[0] is not NoteModel note)
            return;

        await Navigation.PushModalAsync(new NoteView(note));

        ((CollectionView)sender).SelectedItem = 0;
    }


    private List<NotesGroup> GetNotesGroups()
    {
        List<NotesGroup> notesGroups = [.. AppGlobals.Notes
            .GroupBy(note => note.Lesson?.Date.Date) // Группируем по дате
            .Select(group => new NotesGroup($"{group.Key:M}, {group.Key:dddd}" ?? "Unknown", [.. group])) // Создаём NotesGroup
            .OrderBy(group => group.Name)];

        return notesGroups;
    }
}