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
    public static Dictionary<TrashType, string> trashTypeMap = new Dictionary<TrashType, string>()
    {
        { TrashType.Paper, "Paper" },
        { TrashType.Plastic, "Plastic" },
        { TrashType.Glass, "Glass" },
        { TrashType.Metal, "Metal" },
        { TrashType.Organic, "Organic" },
        { TrashType.Electronic, "Electronic" },
    };

    public static string GetString(TrashType value)
    {
        return trashTypeMap.TryGetValue(value, out var result) ? result : throw new ArgumentException($"No mapping defined for {value}");
    }
}