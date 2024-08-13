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


    private static List<NotesGroup> GetNotesGroups()
    {
        List<NotesGroup> notesGroups = [.. AppGlobals.Notes
            .Where(note => note.IsForNextLesson == false)
            .GroupBy(note => note.Lesson?.Date.Date)
            .Select(group => new NotesGroup($"{group.Key:M}, {group.Key:dddd}" ?? "Unknown", [.. group.OrderBy(x => x.Lesson?.Number)]))
            .OrderBy(group => group.Name)];

        NotesGroup forNextLesson = new("Не выявленные", [.. AppGlobals.Notes.Where(note => note.IsForNextLesson == true)]);
        if (forNextLesson.Count > 0)
        {
            notesGroups.Add(forNextLesson);
        }

        return notesGroups;
    }
}