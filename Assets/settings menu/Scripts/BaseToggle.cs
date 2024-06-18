using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseToggle : MonoBehaviour
{
    private Toggle _toggle;

    protected virtual void OnEnable()
    {
        _toggle = GetComponent<Toggle>();
        MainMenuButton.GoToMain += Untoggle;
    }

    private void Untoggle()
    {
        Untoggle(false);
    }

    protected void Untoggle(bool t)
    {
        _toggle.isOn = t;
    }

}
