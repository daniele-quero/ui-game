using System;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public static Action GoToMain;

    public void LaunchGoToMain()
    {
        GoToMain?.Invoke();
        BaseToggle.ButtonPressed?.Invoke();
    }
}
