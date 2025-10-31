using System.Globalization;
using MetroTicketApp.Resources.Strings;

namespace MetroTicketApp;

[QueryProperty(nameof(StationSelectionResult), "StationSelectionResult")]
public partial class MainPage : ContentPage
{
	private readonly string currentLocale;
    private readonly Graph graph;
    private List<Station>? allStations;
    private Station? sourceStation;
    private Station? destinationStation;
    public StationSelectionResult StationSelectionResult
    {
        set
        {
            if (value.Type == StationSelectionType.From)
            {
                sourceStation = value.Station;
            }
            else
            {
                destinationStation = value.Station;
            }
            Update(value.Type);
        }
    }

    public MainPage(Graph graph)
    {
        InitializeComponent();
        currentLocale = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        this.graph = graph;
        if (currentLocale == "ar")
        {
            FlowDirection = FlowDirection.RightToLeft;
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        allStations = await graph.BuildGraphAsync();
    }

    private void OnSwitchLocaleClicked(object sender, EventArgs e)
	{
        string cultureCode;
        if (currentLocale == "en")
        {
            cultureCode = "ar";
        }
        else
        {

            cultureCode = "en";
        }

        var culture = new CultureInfo(cultureCode);

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        Preferences.Set("app_language", cultureCode);

        if (Application.Current is not null)
        {
            Application.Current.Windows[0].Page = new AppShell();
        }

	}

	private async void OnFromClicked(object sender, EventArgs e)
	{
        if (allStations is null)
        {
            throw new Exception("Failed to fetch stations");
        }

        await Navigation.PushAsync(new SelectionPage(StationSelectionType.From, allStations, currentLocale), true);
	}

    private async void OnToClicked(object sender, EventArgs e)
    {
        if (allStations is null)
        {
            throw new Exception("Failed to fetch stations");
        }

        await Navigation.PushAsync(new SelectionPage(StationSelectionType.To, allStations, currentLocale), true);
    }

    private async void OnInfoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InfoPage());
    }

    private void Update(StationSelectionType stationSelectionType)
    {
        if (stationSelectionType == StationSelectionType.From)
        {
            if (currentLocale == "en")
            {
                FromBtn.Text = sourceStation?.NameEn;
            }
            else
            {
                FromBtn.Text = sourceStation?.NameAr;
            }
        }
        else
        {
            if (currentLocale == "en")
            {
                ToBtn.Text = destinationStation?.NameEn;
            }
            else
            {
                ToBtn.Text = destinationStation?.NameAr;
            }
        }

        if (sourceStation is not null && destinationStation is not null)
        {
            var pathStations = Graph.FindPath(sourceStation, destinationStation);
            Stations.Children.Clear();

            uint distance = (uint)pathStations.Count;

            Ticket ticket = Graph.CalcuateTicket(distance);

            string? formattedDistance = distance switch
            {
                0 => AppResources.ResourceManager.GetString("StationCount_Zero"),
                1 => AppResources.ResourceManager.GetString("StationCount_One"),
                2 => AppResources.ResourceManager.GetString("StationCount_Two"),
                _ => string.Format(AppResources.StationCount_Many, distance)
            } ?? throw new Exception("Failed to find localized string for stations count");
            string ticketString = $"{ticket.ToCustomString()}Ticket";
            TicketLabel.Text = $"{AppResources.ResourceManager.GetString(ticketString)}\n{formattedDistance}";
            TicketLabel.TextColor = ticket.ToColor();

            if (currentLocale == "en")
            {
                foreach (var s in pathStations)
                {
                    Stations.Children.Add(new Label
                    {
                        Text = s.NameEn,
                        TextColor = s.Line.ToColor(),
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Padding = 5,
                    });
                }
            }
            else
            {
                foreach (var s in pathStations)
                {
                    Stations.Children.Add(new Label
                    {
                        Text = s.NameAr,
                        TextColor = s.Line.ToColor(),
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Padding = 5,
                    });
                }
            }
        }
    }
}