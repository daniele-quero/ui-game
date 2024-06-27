public class GameChoice : BaseScene
{
    protected override void Awake()
    {
        base.Awake();
        GameChoiceButton.OpenGameChoice += Activate;
        GameLoader.GameLoaderPressed += Deactivate;
    }

    protected override void Activate(bool active) => _canvas.enabled = active;
    private void Deactivate() => _canvas.enabled = false;
}
