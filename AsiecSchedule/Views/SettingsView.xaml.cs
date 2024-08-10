using AsiecSchedule.Data;
using AsiecSchedule.Data.Asiec;
using AsiecSchedule.Update;

namespace AsiecSchedule.Views;

public partial class SettingsView : ContentPage
{
	private readonly Dictionary<string, RequestType> _itemToRequestType = new()
	{
		{ "������",		  RequestType.GroupId	  },
		{ "�������������", RequestType.TeacherId   },
		{ "���������",    RequestType.ClassroomId },
	};

	private readonly Dictionary<RequestType, string> _typeToRequestIDLabelText = new()
	{
		{ RequestType.GroupId, "������:" },
		{ RequestType.TeacherId, "�������������:" },
		{ RequestType.ClassroomId, "���������:" },
	};

	public SettingsView()
	{
		InitializeComponent();
	}


    protected override void OnAppearing()
    {
        base.OnAppearing();

		//RequestType
		RequestTypePicker.ItemsSource = _itemToRequestType.Keys.ToArray();
		RequestTypePicker.Title = AppSettings.RequestType == RequestType.None
			? "�������"
			: _itemToRequestType.FirstOrDefault(x => x.Value == AppSettings.RequestType).Key;

		//RequestID
		if (AppSettings.RequestType != RequestType.None)
		{
			UpdateRequestIDSetting();
			RequestIDPicker.Title = AppSettings.RequestID == string.Empty
				? "�������"
				: AppSettings.RequestID;

			RequestIDSettingFrame.IsVisible = true;
		}
		else
		{
			RequestIDSettingFrame.IsVisible = false;
		}

		//NotifyAboutUpdateCheckBox
		NotifyAboutUpdateCheckBox.IsChecked = AppSettings.IsNotifyAboutUpdate;

		//Debug
		IsDebugPicker.Title = AppSettings.IsDebug
			? "��������"
			: "���������";

		//Version Label
		VersionLabel.Text = $"������: v{AppUpdater.GetCurrentVersion()}";
    }


    private void RequestTypePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
		Picker picker = (Picker)sender;
		int selectedIndex = picker.SelectedIndex;

		if (selectedIndex != -1)
		{
			string item = picker.Items[selectedIndex];
			AppSettings.RequestType = _itemToRequestType[item];
		}

		RequestIDSettingFrame.IsVisible = true;
		UpdateRequestIDSetting();
    }


    private void RequestIDPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            string item = picker.Items[selectedIndex];
            AppSettings.RequestID = item;
        }

		AppGlobals.FlyoutMenuUpdateRequestIDLabel?.Invoke();
    }


	private void UpdateRequestIDSetting()
	{
        RequestIDPicker.ItemsSource = AsiecData.GetIDDict(AppSettings.RequestType).Keys.ToArray();
        ChooseRequestIDText.Text = _typeToRequestIDLabelText[AppSettings.RequestType];
    }

	private void OnCheckUpdatesButton_Clicked(object sender, EventArgs e)
	{
		AppGlobals.CheckUpdates?.Invoke();
	}

    private void IsDebugPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            string item = picker.Items[selectedIndex];
            AppSettings.IsDebug = item == "��������";
        }
    }

    private void IsNotifyAboutUpdateCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
		AppSettings.IsNotifyAboutUpdate = e.Value;
    }
}