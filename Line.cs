namespace MetroTicketApp;

[Flags]
public enum Line
{
    Transition = 1,
    Line1 = 2,
    Line2 = 4,
    Line3 = 8,
}

public static class LineExtensions
{
    private static readonly Line[] lines = [Line.Line1, Line.Line2, Line.Line3];

    public static Color ToColor(this Line line)
    {
        if (line.HasFlag(Line.Transition))
        {
            return new Color(255, 255, 255);
        }

        if (line.HasFlag(Line.Line1))
        {
            return new Color(100, 181, 246);
        }

        if (line.HasFlag(Line.Line2))
        {
            return new Color(220, 88, 48);

        }

        if (line.HasFlag(Line.Line3))
        {
            return new Color(0, 204, 2);
        }

        throw new Exception("Unimplemented for this metro line value");
    }

    public static IEnumerable<Line> ExtractLines(this Line line)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            if (line.HasFlag(lines[i]))
            {
                yield return lines[i++];
            }
        }
    }
}
