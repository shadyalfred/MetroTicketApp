using Plugin.Maui.SimpleSearchPicker;

namespace MetroTicketApp;

public class Station : IStringPresentable
{
    private readonly string name;
    public Line Line;
    public List<Station> Neighbors { get; private set; } = new List<Station>(4);
    private static readonly string[] transitionStations = ["Al-Shohadaa", "Sadat", "Attaba", "Nasser", "Cairo University"];

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


    public Station(string name, string visibleData, Line line)
    {
        this.name = name;
        this.VisibleData = visibleData;
        this.Line = line;
    }

    public string VisibleData { get; private set; }

    private static List<Station> BuildAllStations()
    {

        List<Station> stations = new List<Station>(128);

        string[][] line1Stations =
        [
            ["New El-Marg", "المرج الجديدة"],
            ["El-Marg", "المرج"],
            ["Ezbet El-Nakhl", "عزبة النخل"],
            ["Ain Shams", "عين شمس"],
            ["El-Matareyya", "المطرية"],
            ["Helmeyet El-Zaitoun", "حلمية الزيتون"],
            ["Hadayeq El-Zaitoun", "حدائق الزيتون"],
            ["Saray El-Qobba", "سراي القبة"],
            ["Hammamat El-Qobba", "حمامات القبة"],
            ["Kobri El-Qobba", "كوبري القبة"],
            ["Manshiet El-Sadr", "منشية الصدر"],
            ["El-Demerdash", "الدمرداش"],
            ["Ghamra", "غمرة"],
            ["Al-Shohadaa", "الشهداء"],
            ["Urabi", "أحمد عرابي"],
            ["Nasser", "جمال عبد الناصر"],
            ["Sadat", "أنور السادات"],
            ["Saad Zaghloul", "سعد زغلول"],
            ["AlSayyeda Zeinab", "السيدة زينب"],
            ["El-Malek El-Saleh", "الملك الصالح"],
            ["Mar Girgis", "مار جرجس"],
            ["El-Zahraa'", "الزهراء"],
            ["Dar El-Salam", "دار السلام"],
            ["Hadayeq El-Maadi", "حدائق المعادي"],
            ["Maadi", "المعادي"],
            ["Thakanat El-Maad", "ثكنات المعادي"],
            ["Tora El-Balad", "طرة البلد"],
            ["Kozzika", "كوتسكا"],
            ["Tora El-Asmant", "طرة الأسمنت"],
            ["El-Maasara", "المعصرة"],
            ["Hadayeq Helwan", "حدائق حلوان"],
            ["Wadi Hof", "وادي حوف"],
            ["Helwan University", "جامعة حلوان"],
            ["Ain Helwan", "عين حلوان"],
            ["Helwan", "حلوان"],
        ];

        for (int i = 0; i < line1Stations.Length - 1; i++)
        {
            Station? station1 = stations.Find(s => s.name == line1Stations[i][0]);
            if (station1 is null)
            {
                station1 = new(
                    line1Stations[i][0],
                    line1Stations[i][1],
                    transitionStations.Contains(line1Stations[i + 1][0]) ? Line.Transition | Line.Line1 : Line.Line1);
                stations.Add(station1);
            }
            else
            {
                station1.Line |= Line.Line1;
            }

            Station? station2 = stations.Find(s => s.name == line1Stations[i + 1][0]);
            if (station2 is null)
            {
                station2 = new(
                    line1Stations[i + 1][0],
                    line1Stations[i + 1][1],
                    transitionStations.Contains(line1Stations[i + 1][0]) ? Line.Transition | Line.Line1 : Line.Line1);
                stations.Add(station2);
            }
            else
            {
                station2.Line |= Line.Line1;
            }

            station1.Neighbors.Add(station2);
            station2.Neighbors.Add(station1);
        }

        string[][] line2Stations =
        [
            ["Shobra El Kheima", "شبرا الخيمة"],
            ["Koliet El-Zeraa", "كلية الزراعة"],
            ["Mezallat", "المظلات"],
            ["Khalafawy", "الخلفاوي"],
            ["Sainte Teresa", "سانت تريزا"],
            ["Road El-Farag", "روض الفرج"],
            ["Massara", "مسرة"],
            ["Al-Shohadaa", "الشهداء"],
            ["Attaba", "العتبة"],
            ["Naguib", "محمد نجيب"],
            ["Sadat", "أنور السادات"],
            ["Opera", "الأوبرا"],
            ["Dokki", "الدقي"],
            ["El Behoos", "البحوث"],
            ["Cairo University", "جامعة القاهرة"],
            ["Faisal", "فيصل"],
            ["Giza", "الجيزة"],
            ["Omm el Misryeen", "ضواحي الجيزة"],
            ["Sakiat Mekki", "ساقية مكي"],
            ["El Mounib", "المنيب"],
        ];

        for (int i = 0; i < line2Stations.Length - 1; i++)
        {
            Station? station1 = stations.Find(s => s.name == line2Stations[i][0]);
            if (station1 is null)
            {
                station1 = new(
                    line2Stations[i][0],
                    line2Stations[i][1],
                    transitionStations.Contains(line2Stations[i + 1][0]) ? Line.Transition | Line.Line2 : Line.Line2);
                stations.Add(station1);
            }
            else
            {
                station1.Line |= Line.Line2;
            }

            Station? station2 = stations.Find(s => s.name == line2Stations[i + 1][0]);
            if (station2 is null)
            {
                station2 = new(
                    line2Stations[i + 1][0],
                    line2Stations[i + 1][1],
                    transitionStations.Contains(line2Stations[i + 1][0]) ? Line.Transition | Line.Line2 : Line.Line2);
                stations.Add(station2);
            }
            else
            {
                station2.Line |= Line.Line2;
            }

            station1.Neighbors.Add(station2);
            station2.Neighbors.Add(station1);
        }

        string[][] line3Stations =
        [
            ["Adly Mansour", "عدلي منصور"],
            ["Hikestep", "الهايكستب"],
            ["Omar ibn Al-khattab", "عمر بن الخطاب"],
            ["Kebaa", "قباء"],
            ["Hisham Barakat", "هشام بركات"],
            ["El-Nozha", "النزهة"],
            ["El-Shams Club", "نادي الشمس"],
            ["Alf Masken", "ألف مسكن"],
            ["Heliopolis Square", "هليوبليس"],
            ["Haroun", "هارون"],
            ["Al Ahram", "الأهرام"],
            ["Koleyet El Banat", "كلية البنات"],
            ["Cairo Stadium", "الاستاد"],
            ["Fair Zone", "أرض المعارض"],
            ["Abbassia", "العباسية"],
            ["Abdou Pasha", "عبده باشا"],
            ["El Geish", "الجيش"],
            ["Bab El Shaaria", "باب الشعرية"],
            ["Attaba", "العتبة"],
            ["Nasser", "جمال عبد الناصر"],
            ["Maspero", "ماسبيرو"],
            ["Zamalek", "صفاء حجازي"],
            ["Kit Kat", "الكيت كات"],
            ["Al-Tawfikya", "التوفيقية"],
            ["Wadi Al-Nile", "وادي النيل"],
            ["Gamaet Al-Dowal", "جامعة الدول العربية"],
            ["Bolak Al-Dakror", "بولاق الدكرور"],
            ["Cairo University", "جامعة القاهرة"],
        ];

        for (int i = 0; i < line3Stations.Length - 1; i++)
        {
            Station? station1 = stations.Find(s => s.name == line3Stations[i][0]);
            if (station1 is null)
            {
                station1 = new(
                    line3Stations[i][0],
                    line3Stations[i][1],
                    transitionStations.Contains(line3Stations[i + 1][0]) ? Line.Transition | Line.Line3 : Line.Line3);
                stations.Add(station1);
            }
            else
            {
                station1.Line |= Line.Line3;
            }

            Station? station2 = stations.Find(s => s.name == line3Stations[i + 1][0]);
            if (station2 is null)
            {
                station2 = new(
                    line3Stations[i + 1][0],
                    line3Stations[i + 1][1],
                    transitionStations.Contains(line3Stations[i + 1][0]) ? Line.Transition | Line.Line3 : Line.Line3);
                stations.Add(station2);
            }
            else
            {
                station2.Line |= Line.Line3;
            }

            station1.Neighbors.Add(station2);
            station2.Neighbors.Add(station1);
        }


        string[][] line3HarounExt =
        [
            ["Haroun", "هارون"],
            ["Al-Hegaz Square", "ميدان الحجاز"],
            ["Al-Hegaz 2", "الحجاز"],
            ["Military Academy", "الكلية الحربية"],
            ["Sheraton", "مساكن شيراتون"],
            ["Airport", "مطار القاهرة"],

        ];

        for (int i = 0; i < line3HarounExt.Length - 1; i++)
        {
            Station? station1 = stations.Find(s => s.name == line3HarounExt[i][0]);
            if (station1 is null)
            {
                station1 = new(
                    line3HarounExt[i][0],
                    line3HarounExt[i][1],
                    transitionStations.Contains(line3HarounExt[i + 1][0]) ? Line.Transition | Line.Line3 : Line.Line3);
                stations.Add(station1);
            }
            else
            {
                station1.Line |= Line.Line3;
            }

            Station? station2 = stations.Find(s => s.name == line3HarounExt[i + 1][0]);
            if (station2 is null)
            {
                station2 = new(
                    line3HarounExt[i + 1][0],
                    line3HarounExt[i + 1][1],
                    transitionStations.Contains(line3HarounExt[i + 1][0]) ? Line.Transition | Line.Line3 : Line.Line3);
                stations.Add(station2);
            }
            else
            {
                station2.Line |= Line.Line3;
            }

            station1.Neighbors.Add(station2);
            station2.Neighbors.Add(station1);
        }

        string[][] line3KitKatExt =
        [
            ["Kit Kat", "الكيت كات"],
            ["Sudan", "السودان"],
            ["Imbaba", "إمبابة"],
            ["Al-Bohy", "البوهي"],
            ["Al-Kawmeiah", "القومية العربية"],
            ["Ring Road", "الطريق الدائري"],
            ["Rod Al-Farag Corridor", "محور روض الفرج"],
        ];

        for (int i = 0; i < line3KitKatExt.Length - 1; i++)
        {
            Station? station1 = stations.Find(s => s.name == line3KitKatExt[i][0]);
            if (station1 is null)
            {
                station1 = new(
                    line3KitKatExt[i][0],
                    line3KitKatExt[i][1],
                    transitionStations.Contains(line3KitKatExt[i + 1][0]) ? Line.Transition | Line.Line3 : Line.Line3);
                stations.Add(station1);
            }
            else
            {
                station1.Line |= Line.Line3;
            }

            Station? station2 = stations.Find(s => s.name == line3KitKatExt[i + 1][0]);
            if (station2 is null)
            {
                station2 = new(
                    line3KitKatExt[i + 1][0],
                    line3KitKatExt[i + 1][1],
                    transitionStations.Contains(line3KitKatExt[i + 1][0]) ? Line.Transition | Line.Line3 : Line.Line3);
                stations.Add(station2);
            }
            else
            {
                station2.Line |= Line.Line3;
            }

            station1.Neighbors.Add(station2);
            station2.Neighbors.Add(station1);
        }

        return stations;
    }

    public static Ticket CalcuateTicket(uint distance)
    {
        return distance switch
        {
            0 => Ticket.NoTicket,
            <= 9 => Ticket.Yellow,
            >= 10 and <= 16 => Ticket.Green,
            >= 17 and <= 23 => Ticket.Red,
            _ => Ticket.Beige
        };
    }

    public static IList<Station> FindPath(Station source, Station destination)
    {
        List<Station> stations = new(64);

        if (source == destination)
        {
            return stations;
        }

        PriorityQueue<(Station, Line), uint> priorityQueue = new(64);
        Dictionary<(Station, Line), uint> distances = new(64);
        Dictionary<(Station, Line), (Station, Line)> previous = new(64);

        foreach (var line in source.Line.ExtractLines())
        {
            distances[(source, line)] = 0;
            priorityQueue.Enqueue((source, line), 0);
        }

        (Station, Line)? bestDestination = null;

        while (priorityQueue.TryDequeue(out var current, out var distance))
        {
            var (currentStation, currentLine) = current;

            if (currentStation == destination)
            {
                bestDestination = current;
                break;
            }

            // Switching to a different line
            if (currentStation.Line.HasFlag(Line.Transition))
            {
                foreach (var nextLine in currentStation.Line.ExtractLines())
                {
                    if (nextLine == currentLine)
                    {
                        continue;
                    }

                    var next = (currentStation, nextLine);
                    var newDistance = distance + 1;
                    if (newDistance < distances.GetValueOrDefault(next, uint.MaxValue))
                    {
                        distances[next] = newDistance;
                        previous[next] = current;
                        priorityQueue.Enqueue(next, newDistance);
                    }
                }
            }

            // Checking neighbors
            foreach (var neighbor in currentStation.Neighbors)
            {
                if (!neighbor.Line.HasFlag(currentLine))
                {
                    continue;
                }

                var next = (neighbor, currentLine);
                var newDistance = distance + 1;
                if (newDistance < distances.GetValueOrDefault(next, uint.MaxValue))
                {
                    distances[next] = newDistance;
                    previous[next] = current;
                    priorityQueue.Enqueue(next, newDistance);
                }
            }
        }

        if (bestDestination is null)
        {
            throw new Exception("Failed to find path");
        }

        (Station, Line) c = bestDestination.Value;
        stations.Add(c.Item1);
        while (c.Item1 != source)
        {
            c = previous[c];
            if (stations.Last() != c.Item1)
            {
                stations.Add(c.Item1);
            }
        }
        if (stations.Last() != source)
        {
            stations.Add(source);
        }

        stations.Reverse();

        return stations;
    }
}
