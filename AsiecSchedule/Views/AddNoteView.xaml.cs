using AsiecSchedule.Models;

namespace AsiecSchedule.Views;

public partial class AddNoteView : ContentPage
{
	public AddNoteView()
	{
		InitializeComponent();
	}

	public AddNoteView(LessonModel model)
	{
		InitializeComponent();

		LessonStackLayout.BindingContext = model;
	}
}