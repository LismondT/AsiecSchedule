using System.Windows.Input;

namespace AsiecSchedule.Views;

public partial class AboutView : ContentPage
{
#pragma warning disable CA1822 // Пометьте члены как статические
    public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
#pragma warning restore CA1822 // Пометьте члены как статические
    public ICommand SaveToClipboardCommand => new Command<string>(async (text) =>
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Clipboard.Default.SetTextAsync(text);
        });
        await DisplayAlert("Текст скопирован", null, "ок");
    });

    public AboutView()
	{
		InitializeComponent();
		BindingContext = this;
	}
}