using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayPause : MonoBehaviour
{
    public static Action<bool> PlayPausePressed;

    void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener(Notify);
    }

    private void Notify(bool value)
    {
        Debug.Log("pressing button with value: " + value);
        PlayPausePressed?.Invoke(value);
    }

}
