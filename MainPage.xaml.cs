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

        string source = sourceStationPicker.SelectedItem?.VisibleData ?? throw new Exception("Selected source station is null");
        string destination = destinationStationPicker.SelectedItem?.VisibleData ?? throw new Exception("Selected destination station is null");

        (Ticket ticket, uint distance) = Station.CalcuateTicket(source, destination);

        ticketLabel.Text = $"{ticket} Ticket\n{distance} station[s]";
        ticketLabel.TextColor = Color.Parse(ticket.ToHex());
    }

    private void OnDestinationStationChanged(object sender, IStringPresentable e)
    {
        if (sourceStationPicker.SelectedItem is null)
        {
            return;
        }

        string source = sourceStationPicker.SelectedItem?.VisibleData ?? throw new Exception("Selected source station is null");
        string destination = destinationStationPicker.SelectedItem?.VisibleData ?? throw new Exception("Selected destination station is null");

        (Ticket ticket, uint distance) = Station.CalcuateTicket(source, destination);

        ticketLabel.Text = $"{ticket} Ticket\n{distance} station[s]";
        ticketLabel.TextColor = Color.Parse(ticket.ToHex());
    }
}