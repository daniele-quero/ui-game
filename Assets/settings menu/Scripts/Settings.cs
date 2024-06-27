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
        Debug.Log("messege received");
        _canvas.enabled = active;
    }
}
