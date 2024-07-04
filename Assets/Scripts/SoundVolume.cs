using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    [SerializeField] private VolumeType _volumeType;
    [SerializeField] private Slider _slider;

    public event Action<float, VolumeType> OnValueChanged;
    private void Start()
    {
        _slider.value = PlayerPrefs.GetFloat(_volumeType.ToString(), 0.75f);
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ChangeVolume);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(ChangeVolume);
    }

    private void ChangeVolume(float volume)
    {
        OnValueChanged?.Invoke(volume, _volumeType);
        
        PlayerPrefs.SetFloat(_volumeType.ToString(), volume);
    }
}
