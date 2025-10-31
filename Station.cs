namespace MetroTicketApp;

public class Station
{
    public string NameEn { get; private set; }
    public string NameAr { get; private set; }
    public Line Line { get;  set; }
    public Color LineColor => Line.ToColor();
    public List<Station> Neighbors { get; private set; } = new List<Station>(4);

    public Station(string nameEn, string nameAr, Line line)
    {
        this.NameEn = nameEn;
        this.NameAr = nameAr;
        this.Line = line;
    }
}
