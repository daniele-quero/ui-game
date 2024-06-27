using System;

public class ProfileButton : BaseToggle
{
    public static Action<bool> OpenProfile;

    protected override void Awake()
    {
        base.Awake();
        OpenProfile += Untoggle;
    }

    public void LaunchOpenProfile(bool openProfile)
    {
        OpenProfile?.Invoke(openProfile);
        ButtonPressed?.Invoke();
    }
}
