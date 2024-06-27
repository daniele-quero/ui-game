using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseToggle : MonoBehaviour
{
    private Toggle _toggle;
    public static Action ButtonPressed;
    protected virtual void Awake()
    {
        _toggle = GetComponent<Toggle>();
        MainMenuButton.GoToMain += Untoggle;
    }

    protected void Untoggle() => Untoggle(false);

    protected void Untoggle(bool t) => _toggle.isOn = t;

}
