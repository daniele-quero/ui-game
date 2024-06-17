using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private Canvas _canvas;

    private void OnEnable()
    {
        _canvas = GetComponent<Canvas>();
        GameChoiceButton.OpenGameChoice += Deactivate;
    }

    private void Deactivate(bool otherActive)
    {
        if(otherActive)
        {
            _canvas.enabled = false;
        }
    }
}
