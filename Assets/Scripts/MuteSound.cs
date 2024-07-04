using System;
using UnityEngine;
using UnityEngine.UI;

public class MuteSound : MonoBehaviour
{
    [SerializeField] private Button _muteButton;

    public event Action OnClick; 

    private void OnEnable()
    {
        _muteButton.onClick.AddListener(MuteVolume);
    }

    private void OnDisable()
    {
        _muteButton.onClick.RemoveListener(MuteVolume);
    }

    private void MuteVolume()
    {
        OnClick?.Invoke();
    }
}