public class GameChoice : BaseScene
{
    protected override void OnEnable()
    {
        base.OnEnable();
        GameChoiceButton.OpenGameChoice += Activate;
        GameLoader.GameLoaderPressed += Deactivate;
    }

    protected override void Activate(bool active) => _canvas.enabled = active;
    private void Deactivate() => _canvas.enabled = false;
}
