using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private Slider[] _sliders;

    private const string MasterVolume = "MasterVolume";
    private const string MusicVolume = "MusicVolume";
    private const string EffectsVolume = "EffectsVolume";
    private const string MusicEnable = "MusicEnable";

    private bool _muteVolume;

    private void Start()
    {
        _muteVolume = PlayerPrefs.GetInt(MusicEnable) == 0;

        foreach (var slider in _sliders)
        {
            slider.value = slider.name switch
            {
                "MasterSlider" => PlayerPrefs.GetFloat(MasterVolume),
                "MusicSlider" => PlayerPrefs.GetFloat(MusicVolume),
                "EffectsSlider" => PlayerPrefs.GetFloat(EffectsVolume),
                _ => slider.value
            };
        }

        MuteVolume();
    }

    public void MuteVolume()
    {
        float maxVolume = 0f;
        float minVolume = -80f;

        if (_muteVolume)
        {
            _mixer.audioMixer.SetFloat(MasterVolume, maxVolume);
            _muteVolume = _muteVolume == false;
        }
        else
        {
            _mixer.audioMixer.SetFloat(MasterVolume, minVolume);

            _muteVolume = _muteVolume == false;
        }

        PlayerPrefs.SetInt(MusicEnable, _muteVolume ? 1 : 0);
    }

    public void ChangeMasterVolume(float volume)
    {
        _mixer.audioMixer.SetFloat(MasterVolume, Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat(MasterVolume, volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        _mixer.audioMixer.SetFloat(MusicVolume, Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat(MusicVolume, volume);
    }

    public void ChangeEffectsVolume(float volume)
    {
        _mixer.audioMixer.SetFloat(EffectsVolume, Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat(EffectsVolume, volume);
    }
}