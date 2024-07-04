using UnityEngine;
using UnityEngine.Audio;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private SoundVolume[] _slider;
    [SerializeField] private MuteSound _muteButton;

    private bool _muteVolume;
    private float _minVolume = -80f;
    private float _currentMasterVolume;

    private void Start()
    {
        _muteVolume = PlayerPrefs.GetInt(VolumeType.MusicEnable.ToString()) == 0;
        
        _currentMasterVolume = PlayerPrefs.GetFloat(VolumeType.MasterVolume.ToString());
        
        MuteVolume();
    }

    private void OnEnable()
    {
        _muteButton.OnClick += MuteVolume;
        
        foreach (var slider in _slider)
        {
            slider.OnValueChanged += ChangeVolume;
        }
    }

    private void OnDisable()
    {
        _muteButton.OnClick -= MuteVolume;
        
        foreach (var soundSetting in _slider)
        {
            soundSetting.OnValueChanged -= ChangeVolume;
        }
    }

    private void MuteVolume()
    {
        float maxVolume = Mathf.Log10(_currentMasterVolume) * 20;

        _mixer.audioMixer.SetFloat(VolumeType.MasterVolume.ToString(), _muteVolume ? maxVolume : _minVolume);
        
        _muteVolume = _muteVolume == false;

        PlayerPrefs.SetInt(VolumeType.MusicEnable.ToString(), _muteVolume ? 1 : 0);
    }

    private void ChangeVolume(float volume, VolumeType type)
    {
        if (type == VolumeType.MasterVolume)
        {
            _currentMasterVolume = volume;
            
            PlayerPrefs.SetFloat(VolumeType.MasterVolume.ToString(), _currentMasterVolume);
        }
        
        if (type == VolumeType.MasterVolume && _muteVolume)
        {
            return;
        }
        
        _mixer.audioMixer.SetFloat(type.ToString(), Mathf.Log10(volume) * 20);
    }
}