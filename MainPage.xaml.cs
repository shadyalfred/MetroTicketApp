using Plugin.Maui.SimpleSearchPicker;

namespace MetroTicketApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        sourceStationPicker.ItemsSource = Station.AllStations;
        destinationStationPicker.ItemsSource = Station.AllStations;
    }

    private void OnSourceStationChanged(object sender, IStringPresentable e)
    {
        if (destinationStationPicker.SelectedItem is null)
        {
            return;
        }

        Station source = (Station) e;
        Station destination = (Station) destinationStationPicker.SelectedItem;

        var pathStations = Station.FindPath(source, destination);
        stations.Children.Clear();

        uint distance = (uint) pathStations.Count;

        Ticket ticket = Station.CalcuateTicket(distance);

        ticketLabel.Text = $"{ticket.ToCustomString()} Ticket\n{distance} station[s]";
        ticketLabel.TextColor = ticket.ToColor();

        foreach (var s in pathStations)
        {
            stations.Children.Add(new Label
            {
                Text = s.VisibleData,
                TextColor = s.Line.ToColor(),
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
            });
        }
    }

    private void OnDestinationStationChanged(object sender, IStringPresentable e)
    {
        if (sourceStationPicker.SelectedItem is null)
        {
            return;
        }

        Station source = (Station)sourceStationPicker.SelectedItem;
        Station destination = (Station)e;

        var pathStations = Station.FindPath(source, destination);
        stations.Children.Clear();

        uint distance = (uint) pathStations.Count;

        Ticket ticket = Station.CalcuateTicket(distance);

        ticketLabel.Text = $"{ticket.ToCustomString()} Ticket\n{distance} station[s]";
        ticketLabel.TextColor = ticket.ToColor();

        foreach (var s in pathStations)
        {
            stations.Children.Add(new Label
            {
                Text = s.VisibleData,
                TextColor = s.Line.ToColor(),
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
            });
        }
    }
}