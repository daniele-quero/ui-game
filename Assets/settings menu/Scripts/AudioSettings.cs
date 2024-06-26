using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _sfx;
    [SerializeField] private Slider _master;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Toggle _musicMute;
    [SerializeField] private Toggle _sfxMute;
    [SerializeField] private Toggle _masterMute;

    private void OnEnable()
    {
        _music.onValueChanged.AddListener(SetMusicVolume);
        _sfx.onValueChanged.AddListener(SetSFXVolume);
        _master.onValueChanged.AddListener(SetMasterVolume);
        _masterMute.onValueChanged.AddListener(ToggleMaster);
        _musicMute.onValueChanged.AddListener(ToggleMusic);
        _sfxMute.onValueChanged.AddListener(ToggleSFX);
    }

    private void Start()
    {
        _master.value = 0f;
        _music.value = -15f;
        _sfx.value = -5f;
    }

    private void SetMusicVolume(float v) => _mixer.SetFloat("Music", v);
    private void SetSFXVolume(float v) => _mixer.SetFloat("SFX", v);
    private void SetMasterVolume(float v) => _mixer.SetFloat("Master", v);

    private void ToggleMaster(bool isUnmute)
    {
        _music.interactable = isUnmute;
        _sfx.interactable = isUnmute;
        _master.interactable = isUnmute;

        if (isUnmute)
            _master.onValueChanged.Invoke(_master.value);
        else
            SetMasterVolume(-80);
    }

    private void ToggleMusic(bool isUnmute)
    {
        _music.interactable = isUnmute;

        if (isUnmute)
            _music.onValueChanged.Invoke(_music.value);
        else
            SetMusicVolume(-80);
    }

    private void ToggleSFX(bool isUnmute)
    {
        _sfx.interactable = isUnmute;

        if (isUnmute)
            _sfx.onValueChanged.Invoke(_sfx.value);
        else
            SetSFXVolume(-80);
    }
}
