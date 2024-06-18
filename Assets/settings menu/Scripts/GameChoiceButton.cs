using System;

public class GameChoiceButton : BaseToggle
{
    public static Action<bool> OpenGameChoice;

    override protected void OnEnable()
    {
        base.OnEnable();
        OpenGameChoice += Untoggle;
    }

    public void LaunchOpenGameChoice(bool openGameChoice) => OpenGameChoice?.Invoke(openGameChoice);
}
