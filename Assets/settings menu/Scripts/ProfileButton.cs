using System;

public class ProfileButton : BaseToggle
{
    public static Action<bool> OpenProfile;

    protected override void OnEnable()
    {
        base.OnEnable();
        OpenProfile += Untoggle;
    }

    public void LaunchOpenProfile(bool openProfile)
    {
        OpenProfile?.Invoke(openProfile);
        ButtonPressed?.Invoke();
    }
}
