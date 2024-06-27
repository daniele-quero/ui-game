using UnityEngine;

public class CommonSounds : MonoBehaviour
{
    [Header("Common button sounds")]
    [SerializeField] private AudioSource _tooltipSound;
    [SerializeField] private AudioSource _buttonSound;
    [SerializeField] private AudioSource _mainMusic;

    private void Awake()
    {
        Tooltip.ActiveTooltip += _tooltipSound.Play;
        BaseToggle.ButtonPressed += _buttonSound.Play;
        GameLoader.GameLoaderPressed += PauseMainMusic;
        MainMenuButton.GoToMain += PlayMainMusic;
    }

    private void PauseMainMusic() => _mainMusic.volume = 0;
    private void PlayMainMusic() => _mainMusic.volume = 1;
}
