using MetroTicketApp.Resources.Strings;

namespace MetroTicketApp;

public partial class SelectionPage : ContentPage
{
	private string currentLocale;
	private StationSelectionType selectionType;
	private List<Station> allStations;

	public SelectionPage(StationSelectionType selectionType, List<Station> allStations, string currentLocale)
	{
		InitializeComponent();
		this.selectionType = selectionType;
		this.allStations = allStations;
		this.currentLocale = currentLocale;

		if (selectionType == StationSelectionType.From)
		{
			PageTitle.Text = AppResources.ResourceManager.GetString("SourceBtn");
		}
		else
		{
			PageTitle.Text = AppResources.ResourceManager.GetString("DestinationBtn");
		}

		Stations.ItemTemplate = new DataTemplate(() =>
		{
			var label = new Label
			{
				FontSize = 20,
				HorizontalTextAlignment = TextAlignment.Center,
				Padding = new(0, 10)
			};
			label.SetBinding(Label.TextColorProperty, "LineColor");
			label.SetBinding(Label.TextProperty, currentLocale == "en" ? "NameEn" : "NameAr");
			return label;
		});

		Stations.ItemsSource = allStations;
	}

	private async void OnStationSelected(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.FirstOrDefault() is Station selectedStation)
		{
			var stationSelectionResult = new StationSelectionResult
			{
				Station = selectedStation,
				Type = selectionType
			};

			await Shell.Current.GoToAsync("..", new ShellNavigationQueryParameters
			{
				{"StationSelectionResult", stationSelectionResult}
			});
		}
	}
	
	private void OnStationQueryChanged(object sender, TextChangedEventArgs e)
	{
		if (e.NewTextValue == string.Empty)
		{
			Stations.ItemsSource = allStations;
			return;
		}

		if (currentLocale == "ar")
        {
			Stations.ItemsSource = allStations.Where(s => IsSubstring(e.NewTextValue, s.NameAr)).ToList();
        }
		else
		{
			Stations.ItemsSource = allStations.Where(s =>
			{
				return s.NameEn.Contains(e.NewTextValue, StringComparison.CurrentCultureIgnoreCase);
			}).ToList();
        }
	}

	private static bool IsSubstring(string s1, string s2)
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
}