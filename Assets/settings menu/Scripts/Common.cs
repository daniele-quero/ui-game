public class Common : BaseScene
{
    protected override void Awake()
    {
        base.Awake();
        GameChoiceButton.OpenGameChoice += Activate;
        ProfileButton.OpenProfile += Activate;
        MainMenuButton.GoToMain += Deactivate;
    }

    protected override void Activate(bool active)
    {
        if (active)
            _canvas.enabled = true;
    }

    private void Deactivate() => _canvas.enabled = false;
}
