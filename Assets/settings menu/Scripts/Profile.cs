public class Profile : BaseScene
{
    protected override void OnEnable()
    {
        base.OnEnable();
        ProfileButton.OpenProfile += Activate;
    }

    protected override void Activate(bool active) => _canvas.enabled = active;
}
