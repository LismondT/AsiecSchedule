namespace AsiecSchedule.Views;

public partial class ImagesViewerView : ContentPage
{
    private readonly Stream _stream;

	public ImagesViewerView(string filepath)
	{
		InitializeComponent();

		_stream = File.OpenRead(filepath);
		BindingContext = ImageSource.FromStream(() => _stream);
	}


    protected override void OnDisappearing()
    {
        base.OnDisappearing();

		_stream.Close();
    }


    private async void BackButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }
}