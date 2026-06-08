using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _slider;

    private const string PrefKey = "MasterVolume";
    private const string MixerParam = "MasterVolume";

    private void Start()
    {
        float saved = PlayerPrefs.GetFloat(PrefKey, 1f);
        _slider.value = saved;
        ApplyVolume(saved);

        _slider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnSliderChanged(float value)
    {
        ApplyVolume(value);
        PlayerPrefs.SetFloat(PrefKey, value);
    }

    private void ApplyVolume(float value)
    {
        float db = value > 0.001f ? Mathf.Log10(value) * 20f : -80f;
        _mixer.SetFloat(MixerParam, db);
    }
}