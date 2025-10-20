namespace MetroTicketApp;

public enum StationSelectionType
{
    From,
    To
}

[QueryProperty(nameof(StationSelectionResult), "StationSelectionResult")]
public partial class MainPage : ContentPage
{
    private Station? fromStation;
    private Station? toStation;
    public StationSelectionResult StationSelectionResult
    {
        set
        {
            if (value.Type == StationSelectionType.From)
            {
                fromStation = value.Station;
            }
            else
            {
                toStation = value.Station;
            }
            Update(value.Type);
        }
    }

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnFromClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelectPage(StationSelectionType.From));
    }

    private async void OnToClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelectPage(StationSelectionType.To));
    }

    private void Update(StationSelectionType stationSelectionType)
    {
        if (stationSelectionType == StationSelectionType.From)
        {
            FromButton.Text = fromStation?.Name;
        }
        else
        {
            ToButton.Text = toStation?.Name;
        }

        if (fromStation is not null && toStation is not null)
        {

            var pathStations = Station.FindPath(fromStation, toStation);
            stations.Children.Clear();

            uint distance = (uint)pathStations.Count;

            Ticket ticket = Station.CalcuateTicket(distance);

            TicketLabel.Text = $"{ticket.ToCustomString()} Ticket\n{distance} station[s]";
            TicketLabel.TextColor = ticket.ToColor();

            foreach (var s in pathStations)
            {
                stations.Children.Add(new Label
                {
                    Text = s.Name,
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