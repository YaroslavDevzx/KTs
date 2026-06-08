using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class Deserializer : MonoBehaviour
{
    private void Start()
    {
        string path = Path.Combine(Application.dataPath, "Resources/DataBase_ParsedToJson.txt");

        if (!File.Exists(path))
        {
            Debug.LogError("Файл не найден. Сначала запусти Parser.");
            return;
        }

        string json = File.ReadAllText(path);
        DataBase db = JsonConvert.DeserializeObject<DataBase>(json);

        foreach (var item in db.Items)
            Debug.Log($"{item.Name} — {item.Description}");
    }
}