namespace MetroTicketApp;

public class StationSelectionResult
{
	public required Station Station;
	public StationSelectionType Type;
}

public partial class SelectPage : ContentPage
{
	private readonly StationSelectionType stationSelectionType;

	public SelectPage(StationSelectionType stationSelectionType)
	{
		InitializeComponent();
		this.stationSelectionType = stationSelectionType;
		if (this.stationSelectionType == StationSelectionType.From)
		{
			PageTitle.Text = "Source";
		}
		else
		{
			PageTitle.Text = "Destination";
		}
		Stations.ItemsSource = Station.AllStations;
	}

	private void OnStationQueryChanged(object sender, TextChangedEventArgs e)
	{
		if (e.NewTextValue == string.Empty)
		{
			Stations.ItemsSource = Station.AllStations;
			return;
		}

		Stations.ItemsSource = Station.AllStations.Where(s => IsSubstring(e.NewTextValue, s.Name)).ToList();
	}

	private bool IsSubstring(string s1, string s2)
	{
		for (int i = 0; i < s2.Length; i++)
		{
			int j = 0;
			while (j < s1.Length && i + j < s2.Length && AreCharsEqual(s1[j], s2[i + j]))
            {
				j++;
            }
			if (j == s1.Length)
			{
				return true;
			}
		}
		return false;
	}

	private static bool AreCharsEqual(char c1, char c2)
    {
		if (c1 == c2)
        {
			return true;
        }

		if (c1 == 'أ' || c1 == 'ا')
		{
			return c2 == 'أ' || c2 == 'ا';
		}

		return false;
    }

	private async void OnStationSelected(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.FirstOrDefault() is Station selectedStation)
		{
			var stationSelectionResult = new StationSelectionResult
			{
				Station = selectedStation,
				Type = stationSelectionType
			};

			await Shell.Current.GoToAsync("..", new Dictionary<string, object>
            {
                { "StationSelectionResult", stationSelectionResult}
            });
		}
	}
}