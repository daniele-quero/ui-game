using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChoiceButton : MonoBehaviour
{
    public static Action<bool> OpenGameChoice;


    public void LaunchOpenGameChoice(bool openGameChoice) => OpenGameChoice?.Invoke(openGameChoice);
}
