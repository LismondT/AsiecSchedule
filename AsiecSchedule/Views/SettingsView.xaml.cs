using AsiecSchedule.Data;
using AsiecSchedule.Data.Asiec;

namespace AsiecSchedule.Views;

public partial class SettingsView : ContentPage
{
	private readonly Dictionary<string, RequestType> _itemToRequestType = new()
	{
		{ "группе",		  RequestType.GroupId	  },
		{ "преподавателю", RequestType.TeacherId   },
		{ "аудитории",    RequestType.ClassroomId },
	};

	private readonly Dictionary<RequestType, string> _typeToRequestIDLabelText = new()
	{
		{ RequestType.GroupId, "Группа:" },
		{ RequestType.TeacherId, "Преподаватель:" },
		{ RequestType.ClassroomId, "Аудитория:" },
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
			? "выбрать"
			: _itemToRequestType.FirstOrDefault(x => x.Value == AppSettings.RequestType).Key;

		//RequestID
		if (AppSettings.RequestType != RequestType.None)
		{
			UpdateRequestIDSetting();
			RequestIDPicker.Title = AppSettings.RequestID == string.Empty
				? "выбрать"
				: AppSettings.RequestID;

			RequestIDSettingFrame.IsVisible = true;
		}
		else
		{
			RequestIDSettingFrame.IsVisible = false;
		}

		//Debug
		IsDebugPicker.Title = AppSettings.IsDebug
			? "включить"
			: "выключить";
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

    private void IsDebugPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            string item = picker.Items[selectedIndex];
            AppSettings.IsDebug = item == "включить"
				? true
				: false;
        }
    }
}