namespace MetroTicketApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
	{
		// Update the search text in the ViewModel
		DisplayAlert("Search", $"You searched for: {e.NewTextValue}", "OK");
	}
	private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
	{
		// Handle the selected item (e.g., display it or perform an action)
		DisplayAlert("Selected", $"You selected: {picker.SelectedItem}", "OK");
	}

}

