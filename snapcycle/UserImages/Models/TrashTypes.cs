namespace snapcycle.UserImages.Models;

public enum TrashType
{
    Paper,
    Plastic,
    Glass,
    Metal,
    Organic,
    Electronic
}

public class TrashTypeMapping
{
    private static Dictionary<TrashType, string> _mapString = new Dictionary<TrashType, string>()
    {
        { TrashType.Paper, "Paper" },
        { TrashType.Plastic, "Plastic" },
        { TrashType.Glass, "Glass" },
        { TrashType.Metal, "Metal" },
        { TrashType.Organic, "Organic" },
        { TrashType.Electronic, "Electronic" },
    };
    
    private static Dictionary<TrashType, int> _mapScore = new Dictionary<TrashType, int>
    {
        { TrashType.Paper, 25 },        // Paper is often considered more biodegradable
        { TrashType.Plastic, 80 },      // Plastic is known for causing long-term environmental issues
        { TrashType.Glass, 30 },        // Glass is recyclable but has a moderate environmental impact
        { TrashType.Metal, 20 },        // Metals can be recycled with relatively lower environmental impact
        { TrashType.Organic, 10 },      // Organic waste can decompose and contribute to soil health
        { TrashType.Electronic, 90 }    // Electronics often contain hazardous materials and are less biodegradable
    };

    public static string GetString(TrashType value)
    {
        return _mapString.TryGetValue(value, out var result) ? result : throw new ArgumentException($"No mapping defined for {value}");
    }

    public static int GetScore(TrashType value)
    {
        return _mapScore.TryGetValue(value, out var result) ? result : throw new ArgumentException($"No mapping defined for {value}");
    }
}