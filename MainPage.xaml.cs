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

        (Ticket ticket, uint distance) = Station.CalcuateTicket(source, destination);

        ticketLabel.Text = $"{ticket.ToCustomString()} Ticket\n{distance} station[s]";
        ticketLabel.TextColor = ticket.ToColor();
    }

    private void OnDestinationStationChanged(object sender, IStringPresentable e)
    {
        if (sourceStationPicker.SelectedItem is null)
        {
            return;
        }

        Station source = (Station) sourceStationPicker.SelectedItem;
        Station destination = (Station) e;

        (Ticket ticket, uint distance) = Station.CalcuateTicket(source, destination);

        ticketLabel.Text = $"{ticket.ToCustomString()} Ticket\n{distance} station[s]";
        ticketLabel.TextColor = ticket.ToColor();
    }
}