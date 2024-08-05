namespace AsiecSchedule.Views;

public partial class ImagesViewerView : ContentPage
{
	public ImagesViewerView(ImageSource source)
	{
		InitializeComponent();

		BindingContext = source;
	}

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }
}