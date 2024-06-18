using System;
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
        ProfileButton.OpenProfile += Deactivate;
        MainMenuButton.GoToMain += Activate;
    }

    private void Activate()
    {
        _canvas.enabled = true;
    }

    private void Deactivate(bool otherActive)
    {
        if(otherActive)
        {
            _canvas.enabled = false;
        }
    }
}
