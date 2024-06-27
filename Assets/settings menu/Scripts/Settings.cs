using UnityEngine;

public class Settings : MonoBehaviour
{
    private Canvas _canvas;

    private void Awake()
    {
        PlayPause.PlayPausePressed += Activate;
    }

    private void OnEnable()
    {
        _canvas = GetComponent<Canvas>();
    }

    private void Activate(bool active)
    {
        _canvas.enabled = active;

        if (active)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
