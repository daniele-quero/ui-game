using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private Canvas _canvas;
    private void OnEnable()
    {
        _canvas = GetComponent<Canvas>();
        PlayPause.PlayPausePressed += Activate;
    }

    private void Activate(bool active)
    {
        Debug.Log("messege received");
        _canvas.enabled = active;
    }
}
