using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class Utils
{
    public static void ParseCSV(string csvFileName)
    {
        TextAsset csv = Resources.Load<TextAsset>(csvFileName);
        string[] lines = csv.text.Split('\n');

        var items = new System.Collections.Generic.List<Data>();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] cols = line.Split(',');
            items.Add(new Data(cols[0], cols[1]));
        }

        var db = new DataBase { Items = items.ToArray() };

        string path = Path.Combine(Application.dataPath, "Resources/DataBase_ParsedToJson.txt");

        using StreamWriter sw = new StreamWriter(path, false);
        using JsonWriter jw = new JsonTextWriter(sw) { Formatting = Formatting.Indented };

        JsonSerializer serializer = new JsonSerializer();
        serializer.Serialize(jw, db);

        Debug.Log("CSV parsed =>> " + path);
    }
}