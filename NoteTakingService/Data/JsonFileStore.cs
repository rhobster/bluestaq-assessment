using System.Text.Json;

namespace NoteTakingService.Data;

public static class JsonFileStore
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public static List<T> Read<T>(string fileName)
    {
        var path = GetPath(fileName);

        if (!File.Exists(path))
        {
            return new List<T>();
        }

        var json = File.ReadAllText(path);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<T>();
        }

        return JsonSerializer.Deserialize<List<T>>(json, SerializerOptions) ?? new List<T>();
    }

    public static void Write<T>(string fileName, List<T> items)
    {
        var path = GetPath(fileName);
        var directory = Path.GetDirectoryName(path);

        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var json = JsonSerializer.Serialize(items, SerializerOptions);
        File.WriteAllText(path, json);
    }

    private static string GetPath(string fileName)
    {
        var dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        return Path.Combine(dataDirectory, fileName);
    }
}
