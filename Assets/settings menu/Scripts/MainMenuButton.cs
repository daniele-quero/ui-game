using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public static Action GoToMain;


    public void LaunchGoToMain() => GoToMain?.Invoke();
}
