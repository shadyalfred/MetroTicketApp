namespace MetroTicketApp;

public partial class InfoPage : ContentPage
{
	public InfoPage()
	{
		InitializeComponent();

		YellowTicketPrice.TextColor = Ticket.Yellow.ToColor();
		GreenTicketPrice.TextColor = Ticket.Green.ToColor();
		RedTicketPrice.TextColor = Ticket.Red.ToColor();
		BeigeTicketPrice.TextColor = Ticket.Beige.ToColor();

		WhiteStations.TextColor = Line.Transition.ToColor();
		BlueStations.TextColor = Line.Line1.ToColor();
		RedStations.TextColor = Line.Line2.ToColor();
		GreenStations.TextColor = Line.Line3.ToColor();
	}
}