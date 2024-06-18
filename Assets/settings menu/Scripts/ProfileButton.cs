using System;

public class ProfileButton : BaseToggle
{
    public static Action<bool> OpenProfile;

    override protected void OnEnable()
    {
        base.OnEnable();
        OpenProfile += Untoggle;
    }

    public void LaunchOpenProfile(bool openProfile) => OpenProfile?.Invoke(openProfile);
    
}
