public class Profile : BaseScene
{
    protected override void Awake()
    {
        base.Awake();
        ProfileButton.OpenProfile += Activate;
    }

    protected override void Activate(bool active) => _canvas.enabled = active;
}
