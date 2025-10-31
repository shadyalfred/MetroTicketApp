namespace MetroTicketApp;

public enum Ticket
{
    NoTicket,
    Yellow,
    Green,
    Red,
    Beige
}

public static class TicketExtensions
{
    public static string ToCustomString(this Ticket ticket)
    {
        return ticket switch
        {
            Ticket.NoTicket => "No",
            Ticket.Yellow => "Yellow",
            Ticket.Green => "Green",
            Ticket.Red => "Red",
            Ticket.Beige => "Beige",
            _ => throw new Exception("Unimplemented for this ticket type")
        };
    }

    public static Color ToColor(this Ticket ticket)
    {
        return ticket switch
        {
            Ticket.NoTicket => new Color(255, 255, 255),
            Ticket.Yellow => new Color(255, 237, 0),
            Ticket.Green => new Color(0, 255, 22),
            Ticket.Red => new Color(252, 20, 36),
            Ticket.Beige => new Color(250, 204, 153),
            _ => throw new Exception("Unimplemented for this ticket type")
        };
    }
}
