using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SavedDataController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private string FilePath => Path.Combine(Application.dataPath, "Resources/SoundData.txt");
    private float _lastSavedVolume = -1f;

    private void Start()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            SoundData data = JsonConvert.DeserializeObject<SoundData>(json);
            audioSource.volume = data.Volume;
        }
        else
        {
            SaveVolume();
        }
    }

    private void Update()
    {
        if (!Mathf.Approximately(audioSource.volume, _lastSavedVolume)) SaveVolume();
    }

    private void SaveVolume()
    {
        _lastSavedVolume = audioSource.volume;
        string json = JsonConvert.SerializeObject(new SoundData { Volume = _lastSavedVolume }, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }
}