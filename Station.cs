using Plugin.Maui.SimpleSearchPicker;

namespace MetroTicketApp;

public enum Line
{
    Line1,
    Line2,
    Line3,
    Transition,
}

public enum Ticket
{
    NoTicket,
    Yellow,
    Green,
    Red,
    Beige
}

public static class StatusExtensions
{
    public static string ToString(this Ticket ticket)
    {
        return ticket switch
        {
            Ticket.NoTicket => "No Ticket",
            Ticket.Yellow => "Yellow",
            Ticket.Green => "Green",
            Ticket.Red => "Red",
            Ticket.Beige => "Beige",
            _ => throw new Exception("Unimplemented for this ticket type")
        };
    }

    public static string ToHex(this Ticket ticket)
    {
        return ticket switch
        {
            Ticket.NoTicket => "#FFF",
            Ticket.Yellow => "#FAF43A",
            Ticket.Green => "#C5F1C2",
            Ticket.Red => "#F89B92",
            Ticket.Beige => "#D7C4AF",
            _ => throw new Exception("Unimplemented for this ticket type")
        };
    }
}

public class Station : IStringPresentable
{
    private string name;
    private Line line;
    public List<Station> Neighbors { get; private set; } = new List<Station>(4);
    private static string[] transitionStations = ["Al Shohadaa", "Sadat", "Ataba", "Naser", "Cairo University"];

    private static List<Station> _allStations = new(128);
    public static List<Station> AllStations
    {
        get
        {
            if (_allStations.Count == 0)
            {
                _allStations = BuildAllStations();
                return _allStations;
            }
            return _allStations;
        }
        private set => _allStations = value;
    }


    public Station(string name, Line line)
    {
        this.name = name;
        this.line = line;
    }

    public string VisibleData => this.name;

    private static List<Station> BuildAllStations()
    {

        List<Station> stations = new List<Station>(128);

        string[] line1Stations =
        [
            "New El-Marg",
            "El-Marg",
            "Ezbet El-Nakhl",
            "Ain Shams",
            "El-Matareyya",
            "Helmeyet El-Zaitoun",
            "Hadayeq El-Zaitoun",
            "Saray El-Qobba",
            "Hammamat El-Qobba",
            "Kobri El-Qobba",
            "Manshiet El-Sadr",
            "El-Demerdash",
            "Ghamra",
            "Al Shohadaa",
            "Urabi",
            "Naser",
            "Sadat",
            "Saad Zaghloul",
            "AlSayyeda Zeinab",
            "El-Malek El-Saleh",
            "Mar Girgis",
            "El-Zahraa'",
            "Dar El-Salam",
            "Hadayeq El-Maadi",
            "Maadi",
            "Thakanat El-Maad",
            "Tora El-Balad",
            "Kozzika",
            "Tora El-Asmant",
            "El-Maasara",
            "Hadayeq Helwan",
            "Wadi Hof",
            "Helwan University",
            "Ain Helwan",
            "Helwan",
        ];


        for (int i = 0; i < line1Stations.Length - 1; i++)
        {
            Station? station1 = stations.Find(s => s.VisibleData == line1Stations[i]);
            if (station1 is null)
            {
                station1 = new(line1Stations[i], transitionStations.Contains(line1Stations[i + 1]) ? Line.Transition : Line.Line1);
                stations.Add(station1);
            }

            Station? station2 = stations.Find(s => s.VisibleData == line1Stations[i + 1]);
            if (station2 is null)
            {
                station2 = new(line1Stations[i + 1], transitionStations.Contains(line1Stations[i + 1]) ? Line.Transition : Line.Line1);
                stations.Add(station2);
            }

            station1.Neighbors.Add(station2);
            station2.Neighbors.Add(station1);
        }

        string[] line2Stations =
        [
            "Shobra El Kheima",
            "Koliet El-Zeraa",
            "Mezallat",
            "Khalafawy",
            "Sainte Teresa",
            "Road El-Farag",
            "Massara",
            "Al Shohadaa",
            "Ataba",
            "Naguib",
            "Sadat",
            "Opera",
            "Dokki",
            "El Behoos",
            "Cairo University",
            "Faisal",
            "Giza",
            "Omm el Misryeen",
            "Sakiat Mekki",
            "El Mounib",
        ];

        for (int i = 0; i < line2Stations.Length - 1; i++)
        {
            Station? station1 = stations.Find(s => s.VisibleData == line2Stations[i]);
            if (station1 is null)
            {
                station1 = new(line2Stations[i], transitionStations.Contains(line2Stations[i + 1]) ? Line.Transition : Line.Line1);
                stations.Add(station1);
            }

            Station? station2 = stations.Find(s => s.VisibleData == line2Stations[i + 1]);
            if (station2 is null)
            {
                station2 = new(line2Stations[i + 1], transitionStations.Contains(line2Stations[i + 1]) ? Line.Transition : Line.Line1);
                stations.Add(station2);
            }

            station1.Neighbors.Add(station2);
            station2.Neighbors.Add(station1);
        }

        string[] line3Stations =
        [
            "Adly Mansour",
            "Hikestep",
            "Omar ibn Al-khattab",
            "Kebaa",
            "Hisham Barakat",
            "El-Nozha",
            "El-Shams Club",
            "Alf Masken",
            "Heliopolis Square",
            "Haroun",
            "Al Ahram",
            "Koleyet El Banat",
            "Cairo Stadium",
            "Fair Zone",
            "Abbassia",
            "Abdou Pasha",
            "El Geish",
            "Bab El Shaaria",
            "Ataba",
            "Naser",
            "Maspero",
            "Zamalek",
            "Kit Kat",
            "Al-Tawfikya",
            "Wadi Al-Nile",
            "Gamaet Al-Dowal",
            "Bolak Al-Dakror",
            "Cairo University",
        ];

        for (int i = 0; i < line3Stations.Length - 1; i++)
        {
            Station? station1 = stations.Find(s => s.VisibleData == line3Stations[i]);
            if (station1 is null)
            {
                station1 = new(line3Stations[i], transitionStations.Contains(line3Stations[i + 1]) ? Line.Transition : Line.Line1);
                stations.Add(station1);
            }

            Station? station2 = stations.Find(s => s.VisibleData == line3Stations[i + 1]);
            if (station2 is null)
            {
                station2 = new(line3Stations[i + 1], transitionStations.Contains(line3Stations[i + 1]) ? Line.Transition : Line.Line1);
                stations.Add(station2);
            }

            station1.Neighbors.Add(station2);
            station2.Neighbors.Add(station1);
        }


        string[] line3HarounExt =
        [
            "Haroun",
            "Al-Hegaz Square",
            "Al-Hegaz 2",
            "Military Academy",
            "Sheraton",
            "Airport",
        ];

        for (int i = 0; i < line3HarounExt.Length - 1; i++)
        {
            Station? station1 = stations.Find(s => s.VisibleData == line3HarounExt[i]);
            if (station1 is null)
            {
                station1 = new(line3HarounExt[i], transitionStations.Contains(line3HarounExt[i + 1]) ? Line.Transition : Line.Line1);
                stations.Add(station1);
            }

            Station? station2 = stations.Find(s => s.VisibleData == line3HarounExt[i + 1]);
            if (station2 is null)
            {
                station2 = new(line3HarounExt[i + 1], transitionStations.Contains(line3HarounExt[i + 1]) ? Line.Transition : Line.Line1);
                stations.Add(station2);
            }

            station1.Neighbors.Add(station2);
            station2.Neighbors.Add(station1);
        }

        string[] line3KitKatExt =
        [
            "Kit Kat",
            "Sudan",
            "Imbaba",
            "Al-Bohy",
            "Al-Kawmeiah",
            "Ring Road",
            "Rod Al-Farag Corridor",
        ];

        for (int i = 0; i < line3KitKatExt.Length - 1; i++)
        {
            Station? station1 = stations.Find(s => s.VisibleData == line3KitKatExt[i]);
            if (station1 is null)
            {
                station1 = new(line3KitKatExt[i], transitionStations.Contains(line3KitKatExt[i + 1]) ? Line.Transition : Line.Line1);
                stations.Add(station1);
            }

            Station? station2 = stations.Find(s => s.VisibleData == line3KitKatExt[i + 1]);
            if (station2 is null)
            {
                station2 = new(line3KitKatExt[i + 1], transitionStations.Contains(line3KitKatExt[i + 1]) ? Line.Transition : Line.Line1);
                stations.Add(station2);
            }

            station1.Neighbors.Add(station2);
            station2.Neighbors.Add(station1);
        }

        return stations;
    }

    private static uint CalculateDistanceBetween(Station source, Station destination)
    {
        if (source == destination)
        {
            return 0;
        }

        ISet<Station> visited = new HashSet<Station>(64)
        {
            source
        };

        uint distance = 0;

        Queue<Station> q = new(4);
        foreach (var station in source.Neighbors)
        {
            q.Enqueue(station);
        }

        while (q.Count != 0)
        {
            distance++;

            Queue<Station> newQ = new(32);

            foreach (var station in q)
            {
                if (station == destination)
                {
                    return distance;
                }

                if (visited.Contains(station))
                {
                    continue;
                }

                visited.Add(station);

                foreach (var nextStation in station.Neighbors)
                {
                    if (visited.Contains(nextStation))
                    {
                        continue;
                    }

                    newQ.Enqueue(nextStation);
                }
            }

            q = newQ;
        }

        return distance;
    }

    public static (Ticket ticket, uint distance) CalcuateTicket(string src, string dst)
    {
        Station source = AllStations.First(s => s.VisibleData == src);
        Station destination = AllStations.First(s => s.VisibleData == dst);

        uint distance = CalculateDistanceBetween(source, destination);

        Ticket ticket = distance switch
        {
            0 => Ticket.NoTicket,
            <= 9 => Ticket.Yellow,
            >= 10 and <= 16 => Ticket.Green,
            >= 17 and <= 23 => Ticket.Red,
            _ => Ticket.Beige
        };

        return (ticket, distance);
    }
}