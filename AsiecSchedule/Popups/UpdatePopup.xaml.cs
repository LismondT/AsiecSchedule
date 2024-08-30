using AsiecSchedule.Update;
using CommunityToolkit.Maui.Views;

namespace AsiecSchedule.Popups;

public partial class UpdatePopup : Popup
{
	private readonly HttpClient _httpClient;
	private bool _sourceWasDownloaded;

	public UpdatePopup()
	{
		InitializeComponent();

		_httpClient = new HttpClient();
		_sourceWasDownloaded = false;

		Task.Run(async () =>
		{
			await DownloadSource();
			_sourceWasDownloaded = true;
			await CloseAsync();
		});
	}

    protected override Task OnClosed(object? result, bool wasDismissedByTappingOutsideOfPopup, CancellationToken token = default)
    {
		if (_sourceWasDownloaded)
			AppUpdater.Update();
        return base.OnClosed(result, wasDismissedByTappingOutsideOfPopup, token);
    }

    private async Task DownloadSource()
	{
		string url = AppUpdater.GetLatestVersionDownloadUrl();
		string filepath = AppUpdater.TempFilePath;

		using (HttpResponseMessage response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
		{
			response.EnsureSuccessStatusCode();

			long totalBytes = response.Content.Headers.ContentLength ?? -1L;
			long receivedBytes = 0L;

			using (Stream contentStream = await response.Content.ReadAsStreamAsync(),
				fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				byte[] buffer = new byte[8192];
				int bytesRead;
				while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
				{
					await fileStream.WriteAsync(buffer, 0, bytesRead);
					receivedBytes += bytesRead;

					if (totalBytes != -1)
					{
						double progress = (double)receivedBytes / totalBytes;
						UpdateProgress(progress);
					}
				}
			}
		}
	}

	private void UpdateProgress(double progress)
	{
		MainThread.BeginInvokeOnMainThread(() =>
		{
			DownloadProgressBar.Progress = progress;
			ProgressLabel.Text = $"{progress:P0}";
		});
	}

    private async void OnCancelButton_Clicked(object sender, EventArgs e)
    {
		await CloseAsync();
    }
}